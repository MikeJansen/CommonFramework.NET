using CommonFramework.Sample.Iface;

namespace CommonFramework.Sample.Impl
{
    public class Singleton : ISingleton
    {
        private static int _sequence = 0;
        private readonly int _key;
        public Singleton()
        {
            _key = ++_sequence;
        }

        public string Name
        {
            get { return "Singleton"; }
        }

        public int Key
        {
            get { return _key; }
        }
    }
}
