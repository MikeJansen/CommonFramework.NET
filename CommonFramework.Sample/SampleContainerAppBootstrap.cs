using System.Reflection;
using CommonFramework.Container;
using CommonFramework.Ninject;
using CommonFramework.Sample.NinjectContainer;
using Ninject;

namespace CommonFramework.Sample
{
    public class SampleContainerAppBootstrap: IContainerApplicationBootstrap
    {
        public IContainerManager Initialize(Assembly rootAssembly, bool plain)
        {
            return new NinjectContainerManager(Program.UseCommonBinder ? new StandardKernel() : new StandardKernel(new MyNinjectModule()));
        }

        public bool IsJitInitializable
        {
            get { return true; }
        }
    }
}
