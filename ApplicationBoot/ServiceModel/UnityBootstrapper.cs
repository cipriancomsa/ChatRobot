namespace ApplicationBoot.ServiceModel
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using ApplicationBoot.Container;
    using Microsoft.Practices.Unity;

    public abstract class UnityBootstrapper : IDisposable
    {
        private readonly Assembly[] applicationAssemblies;
        private readonly UnityContainer container;

        protected UnityBootstrapper(params Assembly[] applicationAssemblies)
        {
            this.applicationAssemblies = applicationAssemblies;
            this.RegistrationBuilders = new List<IRegistrationBuilder>();
        }

        public List<IRegistrationBuilder> RegistrationBuilders { set; get; }

        public IUnityContainer Container
        {
            get { return this.container; }
        }

        public Assembly[] ApplicationAssemblies
        {
            get { return this.applicationAssemblies; }
        }

        public void Dispose()
        {
            if (this.container != null)
            {
                this.container.Dispose();
            }
        }

        public void Run()
        {
            foreach (IRegistrationBuilder registrationBuilder in this.RegistrationBuilders)
            {
                registrationBuilder.Register();
            }
        }
    }
}