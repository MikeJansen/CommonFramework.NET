using System;
using CommonFramework.Container;
using Ninject;

namespace CommonFramework.Ninject
{
    /// <summary>
    /// Container Manager for Ninject
    /// </summary>
    public class NinjectContainerManager: IContainerManager
    {
        #region Private Fields

        private readonly IKernel _kernel;

        #endregion

        #region Constructors

        public NinjectContainerManager(IKernel kernel)
        {
            _kernel = kernel;
        }

        public NinjectContainerManager() : this(new StandardKernel()) { }

        #endregion

        #region IContainer Manager implementation

        public T GetInstance<T>()
        {
            return _kernel.Get<T>();
        }

        public T TryGetInstance<T>()
        {
            return _kernel.TryGet<T>();
        }

        public T GetInstance<T>(string key)
        {
            return _kernel.Get<T>(key);
        }

        public T TryGetInstance<T>(string key)
        {
            return _kernel.TryGet<T>(key);
        }

        public void Register(Type iface, Type implementation)
        {
            _kernel.Bind(iface).To(implementation);
        }

        public void Register(Type iface, object instance)
        {
            _kernel.Bind(iface).ToConstant(instance);
        }

        public void Register<TIface, TImpl>()
            where TIface : class
            where TImpl : class, TIface
        {
            _kernel.Bind<TIface>().To<TImpl>();
        }

        public void Register<T>(T instance) where T : class
        {
            _kernel.Bind<T>().ToConstant(instance);
        }

        public void Register<TIface, TImpl>(string key)
            where TIface : class
            where TImpl : class, TIface
        {
            _kernel.Bind<TIface>().To<TImpl>().Named(key);
        }

        public void Release(object instance)
        {
            _kernel.Release(instance);
        }

        public void Dispose()
        {
            _kernel.Dispose();
        }

        #endregion
    }
}
