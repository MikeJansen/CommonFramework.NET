using CommonFramework.Sample.Iface;

namespace CommonFramework.Sample.Impl
{
    public class Keyed2 : IKeyed
    {
        private static int _sequence = 0;
        private readonly string _key;

        public Keyed2()
        {
            _key = string.Format("2-{0}", ++_sequence);
        }

        public string Name
        {
            get { return "Keyed2"; }
        }

        public string Key
        {
            get { return _key; }
        }
    }
}
