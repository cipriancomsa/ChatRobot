namespace ApplicationBoot.ServiceModel
{
    using System.ServiceModel.Channels;

    public interface IServiceBehavior
    {
        Binding GetBindingConfiguration();
        ServiceAddress GetServiceAddress();
    }
}