namespace ApplicationBoot.ServiceModel
{
    using System.Reflection;
    using ApplicationBoot.Container;

    public class ServiceUnityBoostrapper : UnityBootstrapper
    {
        public ServiceUnityBoostrapper(params Assembly[] applicationAssemblies)
            : base(applicationAssemblies)
        {
            this.RegistrationBuilders.Add(new ServiceRegistrationBuilder(applicationAssemblies));
            this.RegistrationBuilders.Add(new WcfServiceRegistrationBuilder(applicationAssemblies));
            this.RegistrationBuilders.Add(new MasterTaksServiceRegistrationBuilder());
        }
    }
}