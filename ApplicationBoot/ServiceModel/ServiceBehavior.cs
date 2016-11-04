namespace ApplicationBoot.ServiceModel
{
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using ApplicationBoot.Annotations;
    using Utils.Assemblies;

    [Service(typeof (IServiceBehavior))]
    public class ServiceBehavior : IServiceBehavior
    {
        public Binding GetBindingConfiguration()
        {
            return new BasicHttpBinding();
        }

        public ServiceAddress GetServiceAddress()
        {
            var hostname = AssemblyHelper.LoadAppSetting<string>("appSettings", "hostname");
            var port = AssemblyHelper.LoadAppSetting<string>("appSettings", "port");
            return new ServiceAddress(hostname, port);
        }
    }
}