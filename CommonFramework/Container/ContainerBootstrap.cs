using System;
using System.Linq;
using System.Reflection;
using CommonFramework.Miscellaneous;

namespace CommonFramework.Container
{
    public enum ContainerState { NotInitialized, Initialized, FailedInitialization, Disposed }

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

        public static IContainerManager Initialize(IContainerManager containerManager)
        {
            return Initialize(containerManager, null, false);
        }

        public static IContainerManager Initialize(Assembly rootAssembly, bool plain)
        {
            return Initialize(null, rootAssembly, plain);
        }

        public static IContainerManager Initialize(Assembly rootAssembly)
        {
            return Initialize(null, rootAssembly, false);
        }

        public static IContainerManager Initialize(bool plain)
        {
            return Initialize(null, null, plain);
        }

        public static IContainerManager Initialize()
        {
            return Initialize(null, null, false);
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
                    if (!initializing) throw new ContainerNotInitializedException();
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

        private static IContainerManager Initialize(IContainerManager containerManager, Assembly rootAssembly, bool plain)
        {
            lock (_syncRoot)
            {
                CheckState(true);

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
                        IContainerApplicationBootstrap bootstrap = GetApplicationBootstrap(rootAssembly ?? AssemblyUtil.GetRootAssembly());
                        _containerManager = bootstrap.Initialize(rootAssembly, plain);
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

        private static IContainerApplicationBootstrap GetApplicationBootstrap(Assembly rootAssembly)
        {
            Type bootstrapType = 
                (from type in rootAssembly.GetTypes() 
                 where typeof(IContainerApplicationBootstrap).IsAssignableFrom(type) 
                 select type).FirstOrDefault();

            if (bootstrapType == null)
            {
                throw new ContainerNoBootstrapFoundException(rootAssembly);
            }

            return (IContainerApplicationBootstrap)Activator.CreateInstance(bootstrapType);
        }

        #endregion
    }
}
