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
        /// <param name="options">Initialization options</param>
        /// <returns>The initialized container manager</returns>
        IContainerManager Initialize(Assembly rootAssembly, ContainerInitializationOptions options);

        /// <summary>
        /// Does this bootstrap loader support being initialized just-in-time without an explicit call to ContainerBootstrap.Initialize() ?
        /// </summary>
        bool IsJitInitializable { get; }
    }
}
