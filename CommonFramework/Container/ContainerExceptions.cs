using System;
using System.Reflection;

namespace CommonFramework.Container
{
    abstract public class ContainerException: Exception
    {
        protected ContainerException(string message) : base(message) { }
    }

    public class ContainerAlreadyInitializedException : ContainerException
    {
        public ContainerAlreadyInitializedException() : base("The container has already been initialized.") { }
    }

    public class ContainerNotInitializedException : ContainerException
    {
        public ContainerNotInitializedException() : base("The container has not been initialized and the boostrap loader does not support JIT initialization.") { }
    }

    public class ContainerFailedToInitializeException : ContainerException
    {
        public ContainerFailedToInitializeException() : base("The container failed to initialize.") { }
    }

    public class ContainerNoBootstrapFoundException : ContainerException
    {
        public ContainerNoBootstrapFoundException(Assembly rootAssembly) : base(string.Format("No container bootstrap found in root assembly ({0}).",
            rootAssembly == null ? "null" : rootAssembly.FullName)) { }
    }

    public class ContainerNoCommonBinderFoundException : ContainerException
    {
        public ContainerNoCommonBinderFoundException(Assembly rootAssembly)
            : base(string.Format("No container common binder found in root assembly ({0}).",
                rootAssembly == null ? "null" : rootAssembly.FullName)) { }
    }

    public class ContainerDisposedException : ContainerException
    {
        public ContainerDisposedException() : base("The container has been disposed.") { }
    }

}
