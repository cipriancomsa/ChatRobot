namespace ApplicationBoot.Container
{
    using System;
    using ApplicationBoot.Annotations;

    public struct ServiceUnityRegistration
    {
        public Type ExportType { set; get; }
        public string ContractName { set; get; }
        public LifeTimeScope Lifetime { set; get; }
    }
}