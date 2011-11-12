using System;
using CommonFramework.Container;
using CommonFramework.Sample.Iface;

namespace CommonFramework.Sample.Impl
{
    public class Glorp
    {
        public Glorp(IContainerManager containerManager, INormal normal1, INormal normal2, ISingleton singleton1, ISingleton singleton2)
        {
            IKeyed keyed1_1 = containerManager.GetInstance<IKeyed>("keyed-1");
            IKeyed keyed1_2 = containerManager.GetInstance<IKeyed>("keyed-1");
            IKeyed keyed2_1 = containerManager.GetInstance<IKeyed>("keyed-2");
            IKeyed keyed2_2 = containerManager.GetInstance<IKeyed>("keyed-2");

            string format = "{0} Name={1}, Key={2}";
            Console.WriteLine(format, "normal1", normal1.Name, normal1.Key);
            Console.WriteLine(format, "normal2", normal2.Name, normal2.Key);
            Console.WriteLine(format, "singleton1", singleton1.Name, singleton1.Key);
            Console.WriteLine(format, "singleton2", singleton2.Name, singleton2.Key);
            Console.WriteLine(format, "keyed1_1", keyed1_1.Name, keyed1_1.Key);
            Console.WriteLine(format, "keyed1_2", keyed1_2.Name, keyed1_2.Key);
            Console.WriteLine(format, "keyed2_1", keyed2_1.Name, keyed2_1.Key);
            Console.WriteLine(format, "keyed2_2", keyed2_2.Name, keyed2_2.Key);
        }
    }
}
