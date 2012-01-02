using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonFramework.Container;
using CommonFramework.Sample.Impl;

namespace CommonFramework.Sample
{
    class Program
    {
        public static bool UseCommonBinder { get; private set; }

        static void Main(string[] args)
        {
            // Should JIT load and use common binder
            IContainerManager containerManager = ContainerBootstrap.Manager;

            containerManager.GetInstance<Glorp>();

            Console.WriteLine("\nHit ENTER to continue.");
            Console.ReadLine();
        }
    }
}
