namespace ApplicationBoot.ServiceModel
{
    using Microsoft.Practices.ServiceLocation;

    public class ServiceLocatorHandler
    {
        public static object GetInstance<TInterface>()
        {
            return ServiceLocator.Current.GetInstance<TInterface>();
        }
    }
}