using System;

namespace CommonFramework.Container
{
    public class ContainerInitializationOptions
    {
        public static readonly ContainerInitializationOptions DefaultOptions =
            new ContainerInitializationOptions
            {
                IsPlain = false,
                UseCommonBinder = false,
                IsJitLoad = false
            };

        public static readonly ContainerInitializationOptions JitOptions =
            new ContainerInitializationOptions
            {
                IsPlain = false,
                UseCommonBinder = null,
                IsJitLoad = true
            };

        public bool IsPlain { get; set; }
        public bool IsJitLoad { get; set; }
        public bool? UseCommonBinder { get; set; }

        public bool CheckForCommonBinder { get { return UseCommonBinder == null | (UseCommonBinder.HasValue && UseCommonBinder.Value); } }
        public bool UseCommonBinderActual { get { return UseCommonBinder != null && UseCommonBinder.HasValue && UseCommonBinder.Value; } }
    }
}
 
