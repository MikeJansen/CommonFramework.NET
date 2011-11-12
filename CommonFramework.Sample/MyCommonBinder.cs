using System;
using CommonFramework.Container;
using CommonFramework.Sample.Iface;
using CommonFramework.Sample.Impl;

namespace CommonFramework.Sample
{
    /// <summary>
    /// Sample class to setup the IoC container for the app regardless of which container is used.
    /// </summary>
    public class MyCommonBinder
    {
        public void Bind(IContainerManager cm)
        {
            Console.WriteLine("MyCommonBinder binding");
            cm.Register<INormal, Normal>();
            cm.Register<IKeyed, Keyed1>("keyed-1");
            cm.Register<IKeyed, Keyed2>("keyed-2");
            cm.Register<ISingleton>(new Singleton());
            cm.Register<Glorp, Glorp>();
        }
    }
}
