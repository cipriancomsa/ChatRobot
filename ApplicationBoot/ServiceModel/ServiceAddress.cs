namespace ApplicationBoot.ServiceModel
{
    public struct ServiceAddress
    {
        public ServiceAddress(string hostname, string port) : this()
        {
            this.HostName = hostname;
            this.Port = port;
        }

        public string GetAddress
        {
            get { return this.HostName + this.Port; }
        }

        public string HostName { private set; get; }
        public string Port { private set; get; }
    }
}