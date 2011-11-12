using CommonFramework.Sample.Iface;

namespace CommonFramework.Sample.Impl
{
    public class Normal: INormal
    {
        private static int _sequence = 0;
        private readonly int _key;
        public Normal()
        {
            _key = ++_sequence;
        }

        public string Name
        {
            get { return "Normal"; }
        }

        public int Key
        {
            get { return _key; }
        }
    }
}
