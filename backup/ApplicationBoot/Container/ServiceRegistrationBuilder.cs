namespace ApplicationBoot.Container
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using ApplicationBoot.Annotations;
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;
    using Utils.Assemblies;
    using Utils.Collection;

    public class ServiceRegistrationBuilder : IRegistrationBuilder
    {
        private static readonly Dictionary<Type, HashSet<Type>> internalTypeMapping = new Dictionary<Type, HashSet<Type>>();

        private static readonly Dictionary<LifeTimeScope, Func<ServiceUnityRegistration, LifetimeManager>> lifetimeManagers
            = new Dictionary<LifeTimeScope, Func<ServiceUnityRegistration, LifetimeManager>>
            {
                {LifeTimeScope.None, s => new PerResolveLifetimeManager()},
                {LifeTimeScope.Application, s => new ContainerControlledLifetimeManager()},
                /*{LifeTimeScope.Request, s => new RequestLifetimeManager(s.ContractName ?? s.From.FullName)},
                                       {LifeTimeScope.Session, s => new SessionLifetimeManager(s.ContractName ?? s.From.FullName)},
                                       {LifeTimeScope.Cache, s => new CacheLifetimeManager(s.ContractName ?? s.From.FullName)},*/
            };

        private readonly Assembly[] assemblies;
        private readonly UnityContainer container;

        public ServiceRegistrationBuilder(Assembly[] assemblies)
        {
            this.assemblies = assemblies;
            this.container = new UnityContainer();
        }

        public void Register()
        {
            this.container.RegisterInstance<IServiceLocator>(new UnityServiceLocator(this.container));
            ServiceLocator.SetLocatorProvider(() => this.container.Resolve<IServiceLocator>());
            IEnumerable<Type> allAttributesSubTypes = AssemblyExt.FindSubClassesOf<ServiceAttribute>();
            allAttributesSubTypes = allAttributesSubTypes.Prepend(typeof (ServiceAttribute));
            IEnumerable<Type> serviceAttributeAssemblies = this.assemblies.GetAssembliesTypes(allAttributesSubTypes);
            foreach (Type assembly in serviceAttributeAssemblies)
            {
                IEnumerable<Type> interfacesToBeRegsitered = GetInterfacesToBeRegistered(assembly);
                AddToInternalTypeMapping(assembly, interfacesToBeRegsitered);
            }
            this.RegisterConventions();
        }

        private void RegisterConventions()
        {
            foreach (KeyValuePair<Type, HashSet<Type>> typeMapping in internalTypeMapping)
            {
                if (typeMapping.Value.Count == 1)
                {
                    Type type = typeMapping.Value.First();

                    this.container.RegisterType(typeMapping.Key, type);
                }
                else
                {
                    foreach (Type type in typeMapping.Value)
                    {
                        ServiceUnityRegistration attributes = this.GetAttributes(type);
                        LifetimeManager lifeTimeManager = CreateLifetimeManager(attributes);
                        this.container.RegisterType(typeMapping.Key, type, attributes.ContractName, lifeTimeManager, new InjectionMember[] {});
                    }
                }
            }
        }


        private static string GetNameForRegsitration(Type type)
        {
            return type.Name;
        }

        private ServiceUnityRegistration GetAttributes(Type type)
        {
            var typeAttributes = type.GetAttribute<ServiceAttribute>();

            return new ServiceUnityRegistration
            {
                Lifetime = typeAttributes.Scope,
                ExportType = typeAttributes.ExportType,
                ContractName = String.IsNullOrEmpty(typeAttributes.ContractName) ? GetNameForRegsitration(type) : typeAttributes.ContractName
            };
        }

        private static IEnumerable<Type> GetInterfacesToBeRegistered(Type type)
        {
            List<Type> allInterfacesOnType = type.GetInterfaces()
                .Select(i => i.IsGenericType ? i.GetGenericTypeDefinition() : i).ToList();

            return allInterfacesOnType.Except(allInterfacesOnType.SelectMany(i => i.GetInterfaces())).ToList();
        }

        private static void AddToInternalTypeMapping(Type type, IEnumerable<Type> interfacesOnType)
        {
            foreach (Type interfaceOnType in interfacesOnType)
            {
                if (!internalTypeMapping.ContainsKey(interfaceOnType))
                {
                    internalTypeMapping[interfaceOnType] = new HashSet<Type>();
                }

                internalTypeMapping[interfaceOnType].Add(type);
            }
        }

        public static LifetimeManager CreateLifetimeManager(ServiceUnityRegistration type)
        {
            Func<ServiceUnityRegistration, LifetimeManager> factory = lifetimeManagers[type.Lifetime];
            return factory(type);
        }
    }
}