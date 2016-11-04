namespace ApplicationBoot.Annotations
{
    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ServiceAttribute : Attribute
    {
        public const LifeTimeScope DefaultScope = LifeTimeScope.None;

        public ServiceAttribute(Type exportType)
            : this((string) null, exportType)
        {
        }

        public ServiceAttribute(string contractName)
            : this(contractName, (Type) null)
        {
        }

        public ServiceAttribute(LifeTimeScope scope)
            : this((string) null, (Type) null, scope)
        {
        }

        public ServiceAttribute(string contractName, LifeTimeScope scope)
            : this(contractName, (Type) null, scope)
        {
        }

        public ServiceAttribute(string contractName, Type exportType)
            : this(contractName, exportType, LifeTimeScope.None)
        {
        }

        public ServiceAttribute(Type exportType, LifeTimeScope scope)
            : this((string) null, exportType, scope)
        {
        }

        public ServiceAttribute(string contractName, Type exportType, LifeTimeScope scope)
        {
            this.ContractName = contractName;
            this.ExportType = exportType;
            this.Scope = scope;
        }

        public string ContractName { get; private set; }

        public Type ExportType { get; private set; }

        public LifeTimeScope Scope { get; private set; }
    }
}