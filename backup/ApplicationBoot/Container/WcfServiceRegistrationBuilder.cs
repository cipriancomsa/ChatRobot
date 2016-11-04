namespace ApplicationBoot.Container
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using ApplicationBoot.Annotations;
    using ApplicationBoot.ServiceModel;
    using Utils.Assemblies;

    public class WcfServiceRegistrationBuilder : IRegistrationBuilder
    {
        private readonly IEnumerable<Assembly> wcfAssemblies;

        public WcfServiceRegistrationBuilder(Assembly[] wcfAssemblies)
        {
            this.wcfAssemblies = wcfAssemblies;
        }

        public void Register()
        {
            IEnumerable<Type> wcfServicesAssemblies = this.wcfAssemblies.GetAssembliesTypes<WcfServiceAttribute>();

            foreach (Type wcfService in wcfServicesAssemblies)
            {
                //HostService(wcfService);
            }
        }

        private static void HostService(Type serviceType)
        {
            var hostingHandler = new BasicServiceHost(serviceType);
            hostingHandler.Open();
        }
    }
}