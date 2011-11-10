using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CommonFramework.Container
{
    public interface IContainerApplicationBootstrap
    {
        IContainerManager Initialize(Assembly rootAssembly, bool plain);
    }
}
