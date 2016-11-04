namespace ApplicationBoot.ServiceModel
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Description;
    using System.ServiceModel.Dispatcher;
    using Microsoft.Practices.ServiceLocation;

    public class UnityServiceProvider : IInstanceProvider, IEndpointBehavior
    {
        private readonly Type serviceType;

        public UnityServiceProvider(Type serviceType)
        {
            this.serviceType = serviceType;
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.DispatchRuntime.InstanceProvider = this;
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }

        public Object GetInstance(InstanceContext instanceContext)
        {
            return this.GetInstance(instanceContext, null);
        }

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return ServiceLocator.Current.GetInstance(this.serviceType);
        }

        public void ReleaseInstance(InstanceContext instanceContext, Object instance)
        {
            var disposable = instance as IDisposable;
            if (disposable != null)
                disposable.Dispose();
        }
    }
}