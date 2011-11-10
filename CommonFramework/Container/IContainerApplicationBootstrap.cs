using System.Reflection;

namespace CommonFramework.Container
{
    /// <summary>
    /// Application provided class that the ContainerBootstrap searches for, instantiates, and calls to initialize the container manager
    /// </summary>
    public interface IContainerApplicationBootstrap
    {
        /// <summary>
        /// Initialize the container manager
        /// </summary>
        /// <param name="rootAssembly">Root assembly (may be null)</param>
        /// <param name="plain">Is this a plain initialization (unit testing, for example) or should the default bindings be loaded?</param>
        /// <returns>The initialized container manager</returns>
        IContainerManager Initialize(Assembly rootAssembly, bool plain);

        /// <summary>
        /// Does this bootstrap loader support being initialized just-in-time without an explicit call to ContainerBootstrap.Initialize() ?
        /// </summary>
        bool IsJitInitializable { get; }
    }
}
