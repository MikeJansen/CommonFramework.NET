using System;

namespace CommonFramework.Container
{
    /// <summary>
    /// Abstraction for IoC/DI containers (abstracting the abstraction)
    /// </summary>
    public interface IContainerManager: IDisposable
    {
        T GetInstance<T>();
        T TryGetInstance<T>();
        T GetInstance<T>(string key);
        T TryGetInstance<T>(string key);
        void Register(Type iface, Type implementation);
        void Register(Type iface, object instance);
        void Register<TIface, TImpl>()
            where TIface : class
            where TImpl : class,TIface;
        void Register<T>(T instance) where T : class;
        void Register<TIface, TImpl>(string key)
            where TIface : class
            where TImpl : class,TIface;
        void Release(object instance);
    }
}
