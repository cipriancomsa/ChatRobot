namespace ApplicationBoot.Annotations
{
    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class WcfServiceAttribute : ServiceAttribute
    {
        public WcfServiceAttribute(Type exportType)
            : base(exportType, LifeTimeScope.None)
        {
        }
    }
}