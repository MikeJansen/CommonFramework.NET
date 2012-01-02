using System;
using System.Reflection;
using CommonFramework.Container;
using CommonFramework.Ninject;

namespace CommonFramework.Sample
{
    public class SampleContainerAppBootstrap: IContainerApplicationBootstrap
    {
        public IContainerManager Initialize(Assembly rootAssembly, ContainerInitializationOptions options)
        {
            Console.WriteLine("SampleContainerAppBootstrap.Initialize");
            Console.WriteLine("IsJitLoad = {0}", options.IsJitLoad);
            Console.WriteLine("UseCommonBinder = {0}", options.UseCommonBinder);
            Console.WriteLine("IsPlain = {0}", options.IsPlain);
            return new NinjectContainerManager();
        }

        public bool IsJitInitializable
        {
            get { return true; }
        }
    }
}
