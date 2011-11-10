﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonFramework.Container
{
    public interface IContainerManager: IDisposable
    {
        T GetInstance<T>();
        T GetInstance<T>(string key);
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