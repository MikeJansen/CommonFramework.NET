using System;
using System.Collections.Generic;

namespace CommonFramework.Container
{
    /// <summary>
    /// Abstraction for IoC/DI containers (abstracting the abstraction)
    /// </summary>
    public interface IContainerManager: IDisposable
    {
        T GetInstance<T>();
        object GetInstance(Type type);
        T TryGetInstance<T>();
        object TryGetInstance(Type type);
        T GetInstance<T>(string key);
        object GetInstance(Type type, string key);
        T TryGetInstance<T>(string key);
        object TryGetInstance(Type type, string key);
        IEnumerable<T> GetAll<T>();
        IEnumerable<object> GetAll(Type type);
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
