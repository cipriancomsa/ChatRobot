namespace ApplicationBoot.ServiceModel
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Channels;

    public class BasicServiceHost : ServiceHost
    {
        private readonly IServiceBehavior serviceBehavior;
        private readonly Type serviceType;

        public BasicServiceHost(Type serviceType, params Uri[] baseAddresses) : base(serviceType, baseAddresses)
        {
            this.serviceType = serviceType;
            this.serviceBehavior = new ServiceBehavior();
        }

        private void AddServiceBehaviour()
        {
            Binding binding = this.serviceBehavior.GetBindingConfiguration();
            Type primaryInterface = this.serviceType.GetPrimaryInterface();
            ServiceAddress serviceAddress = this.serviceBehavior.GetServiceAddress();
            this.AddServiceEndpoint(primaryInterface, binding, this.BuildEndPointAddress(serviceAddress))
                .Behaviors.Add(new UnityServiceProvider(this.serviceType));
        }

        public new void Open()
        {
            this.AddServiceBehaviour();
            base.Open();
        }

        private string BuildEndPointAddress(ServiceAddress serviceAddress)
        {
            return serviceAddress.GetAddress + this.serviceType.Name;
        }
    }
}