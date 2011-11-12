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
            Console.Write("Use common binder? [Y/n]?");
            string input = Console.ReadLine();
            UseCommonBinder = input.Length == 0 || Char.ToUpper(input[0]) == 'Y';

            IContainerManager containerManager = ContainerBootstrap.Initialize();

            if (UseCommonBinder)
            {
                new MyCommonBinder().Bind(containerManager);
            }

            containerManager.GetInstance<Glorp>();

            Console.WriteLine("\nHit ENTER to continue.");
            Console.ReadLine();
        }
    }
}
