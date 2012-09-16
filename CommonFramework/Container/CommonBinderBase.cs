using System;
using System.Collections.Generic;

namespace CommonFramework.Container
{
    public abstract class CommonBinderBase : ICommonBinder
    {
        private delegate void BindDelegate(IContainerManager containerManager);
        private readonly Dictionary<object, BindDelegate> _bindDelegates = new Dictionary<object, BindDelegate>();

        private static readonly List<Type> _bindersAlreadyRun = new List<Type>();

        protected void AddBinding<TIface, TImpl>()
            where TIface : class
            where TImpl : class,TIface
        {
            _bindDelegates[typeof(TIface)] = (cm => cm.Register<TIface, TImpl>());
        }

        private KeyValuePair<string, Type> GetKey<T>(string key)
        {
            return new KeyValuePair<string, Type>(key, typeof(T));
        }

        protected void AddBinding<TIface, TImpl>(string key)
            where TIface : class
            where TImpl : class,TIface
        {
            _bindDelegates[GetKey<TIface>(key)] = (cm => cm.Register<TIface, TImpl>(key));
        }

        protected void AddBinding<TIface, TInst>(TInst instance)
            where TIface : class
            where TInst : class, TIface
        {
            _bindDelegates[typeof(TIface)] = (cm => cm.Register<TIface>(instance));
        }

        public void RemoveBinding(object key)
        {
            _bindDelegates.Remove(key);
        }

        public void RemoveBinding<TIface>()
        {
            RemoveBinding(typeof(TIface));
        }

        public void RemoveBinding<T>(string key)
        {
            RemoveBinding(GetKey<T>(key));
        }

        public void Bind(IContainerManager cm)
        {
            // Prevent the same CommonBinder from running twice
            lock (_bindersAlreadyRun)
            {
                if (_bindersAlreadyRun.Contains(this.GetType()))
                    return;
                _bindersAlreadyRun.Add(this.GetType());
            }

            foreach (BindDelegate bind in _bindDelegates.Values)
            {
                bind(cm);
            }
            OnBind(cm);
        }

        protected virtual void OnBind(IContainerManager cm)
        {
        }
    }
}
