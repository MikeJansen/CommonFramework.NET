using System;
using CommonFramework.Sample.Iface;
using CommonFramework.Sample.Impl;
using Ninject.Modules;

namespace CommonFramework.Sample.NinjectContainer
{
    public class MyNinjectModule: NinjectModule
    {
        public override void Load()
        {
            Console.WriteLine("NinjectModule binding");
            Bind<INormal>().To<Normal>();
            Bind<IKeyed>().To<Keyed1>().Named("keyed-1");
            Bind<IKeyed>().To<Keyed2>().Named("keyed-2");
            Bind<ISingleton>().ToConstant(new Singleton());
            Bind<Glorp>().ToSelf();
        }
    }
}
