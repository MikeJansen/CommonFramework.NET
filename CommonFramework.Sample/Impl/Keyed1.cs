using CommonFramework.Sample.Iface;

namespace CommonFramework.Sample.Impl
{
    public class Keyed1: IKeyed
    {
        private static int _sequence = 0;
        private readonly string _key;

        public Keyed1()
        {
            _key = string.Format("1-{0}", ++_sequence);
        }

        public string Name
        {
            get { return "Keyed1"; }
        }

        public string Key
        {
            get { return _key; }
        }
    }
}
