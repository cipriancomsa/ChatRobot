namespace ApplicationBoot.ServiceModel
{
    using System;
    using System.Linq;

    public static class ServiceHelperExt
    {
        public static Type GetPrimaryInterface(this Type serviceType)
        {
            Type[] allServiceInterfaces = serviceType.GetInterfaces();
            return allServiceInterfaces.First();
        }
    }
}