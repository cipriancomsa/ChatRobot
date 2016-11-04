namespace ApplicationBoot.Annotations
{
    using System;

    public interface IServiceAttribute
    {
        Type ContractType { get; }
    }
}