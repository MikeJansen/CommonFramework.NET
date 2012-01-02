using System;
using System.Linq;
using System.Reflection;
using CommonFramework.Miscellaneous;

namespace CommonFramework.Container
{
    public enum ContainerState { NotInitialized, Initialized, FailedInitialization, PossibleJitInitialization, Disposed }

    public static class ContainerBootstrap
    {
        #region Private Fields

        private static readonly object _syncRoot = new object();
        private static ContainerState _state = ContainerState.NotInitialized;
        private static IContainerManager _containerManager;

        #endregion

        #region Public Properties

        public static IContainerManager Manager { get { CheckState(false); return _containerManager; } }
        public static ContainerState State { get { return _state; } }

        #endregion

        #region Public Methods

        public static IContainerManager Initialize(IContainerManager containerManager, Assembly rootAssembly, ContainerInitializationOptions options)
        {
            lock (_syncRoot)
            {
                if (!options.IsJitLoad)
                {
                    CheckState(true);
                }

                // If the container manager has been provided, it's a piece of cake!
                if (containerManager != null)
                {
                    _containerManager = containerManager;
                }
                else
                {
                    // Not provided, we need to find the Container Bootstrap in the root assembly
                    try
                    {
                        IContainerApplicationBootstrap bootstrap = GetApplicationBootstrap(rootAssembly);
                        if (options.IsJitLoad && !bootstrap.IsJitInitializable)
                        {
                            throw new ContainerNotInitializedException();
                        }

                        ICommonBinder binder = options.CheckForCommonBinder ? GetCommonBinder(rootAssembly) : null;
                        _containerManager = bootstrap.Initialize(rootAssembly, options);
                        if ((options.IsJitLoad && binder != null) || options.UseCommonBinderActual)
                        {
                            if (binder != null)
                            {
                                binder.Bind(_containerManager);
                            }
                            else
                            {
                                throw new ContainerNoCommonBinderFoundException(rootAssembly);
                            }
                        }
                    }
                    catch
                    {
                        _state = ContainerState.FailedInitialization;
                        throw;
                    }
                }

                // Register the container manager in itself
                _containerManager.Register<IContainerManager>(_containerManager);
                _state = ContainerState.Initialized;
            }

            return _containerManager;

        }

        public static IContainerManager Initialize(IContainerManager containerManager)
        {
            return Initialize(containerManager, null, ContainerInitializationOptions.DefaultOptions);
        }

        public static IContainerManager Initialize(Assembly rootAssembly, ContainerInitializationOptions options)
        {
            return Initialize(null, rootAssembly, options);
        }

        public static IContainerManager Initialize(Assembly rootAssembly)
        {
            return Initialize(null, rootAssembly, ContainerInitializationOptions.DefaultOptions);
        }

        public static IContainerManager Initialize(ContainerInitializationOptions options)
        {
            return Initialize(null, null, ContainerInitializationOptions.DefaultOptions);
        }

        public static IContainerManager Initialize()
        {
            return Initialize(null, null, ContainerInitializationOptions.DefaultOptions);
        }

        public static void Dispose()
        {
            if (_containerManager != null)
            {
                lock (_syncRoot)
                {
                    try
                    {
                        _containerManager.Dispose();
                    }
                    catch 
                    { 
                        // TODO: Log this
                    }
                    _containerManager = null;
                    _state = ContainerState.Disposed;
                }
            }

        }

        #endregion

        #region Private Methods

        private static void CheckState(bool initializing)
        {
            switch (_state)
            {
                case ContainerState.NotInitialized:
                    if (!initializing) Initialize(null, null, ContainerInitializationOptions.JitOptions); // attempt a JIT initialize
                    break;
                case ContainerState.Initialized:
                    if (initializing) throw new ContainerAlreadyInitializedException();
                    break;
                case ContainerState.FailedInitialization:
                    throw new ContainerFailedToInitializeException();
                default:
                    throw new ContainerDisposedException();
            }
        }

        private static T FindAndInstantiateType<T>(Assembly rootAssembly)
        {
            if (rootAssembly == null) rootAssembly = AssemblyUtil.GetRootAssembly();

            Type bootstrapType = 
                (from type in rootAssembly.GetTypes() 
                 where typeof(T).IsAssignableFrom(type) 
                 select type).FirstOrDefault();

            return (T)(bootstrapType == null ? null : Activator.CreateInstance(bootstrapType));
        }

        private static IContainerApplicationBootstrap GetApplicationBootstrap(Assembly rootAssembly)
        {
            IContainerApplicationBootstrap bootstrap = FindAndInstantiateType<IContainerApplicationBootstrap>(rootAssembly);
            if (bootstrap == null)
            {
                throw new ContainerNoBootstrapFoundException(rootAssembly);
            }
            return bootstrap;
        }

        private static ICommonBinder GetCommonBinder(Assembly rootAssembly)
        {
            return FindAndInstantiateType<ICommonBinder>(rootAssembly);
        }

        #endregion
    }
}
