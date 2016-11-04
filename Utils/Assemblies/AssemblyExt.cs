namespace Utils.Assemblies
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    public static class AssemblyExt
    {
        public static IEnumerable<Type> GetAssembliesTypes<TAttribute>(this IEnumerable<Assembly> assemblies)
            where TAttribute : Attribute
        {
            return assemblies.SelectMany(s => s.GetTypes().Where(t => t.IsClass && (Attribute.IsDefined(t, typeof (TAttribute), false))));
        }

        public static IEnumerable<Type> FindDerivedTypes(Assembly assembly, Type baseType)
        {
            return assembly.GetTypes().Where(baseType.IsAssignableFrom);
        }

        public static IEnumerable<Type> FindSubClassesOf<TBaseType>()
        {
            Type baseType = typeof (TBaseType);
            Assembly assembly = baseType.Assembly;

            return assembly.GetTypes().Where(t => t.IsSubclassOf(baseType));
        }

        public static IEnumerable<Type> GetAssembliesTypes(this IEnumerable<Assembly> assemblies, IEnumerable<Type> attributeTypes)
        {
            var assembliesResult = new List<Type>();
            foreach (Type attributeType in attributeTypes)
            {
                IEnumerable<Type> assembliesWithCurrentAttributeType = GetAssembliesType(assemblies, attributeType);
                assembliesResult.AddRange(assembliesWithCurrentAttributeType);
            }
            return assembliesResult;
        }

        private static IEnumerable<Type> GetAssembliesType(IEnumerable<Assembly> assemblies, Type attributeType)
        {
            try
            {
                return
                    assemblies.SelectMany(
                        s => s.GetTypes().Where(t => t.IsClass && (Attribute.IsDefined(t, attributeType, false))));
            }
            catch (FileNotFoundException ex)
            {
                return null;
            }
        }

        public static TAttribute GetAttribute<TAttribute>(object instance)
        {
            return GetAttribute<TAttribute>(instance, false);
        }

        public static TAttribute GetAttribute<TAttribute>(object instance, bool inherit)
        {
            Contract.Requires(instance != null);
            return GetAttribute<TAttribute>(instance.GetType(), inherit);
        }

        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(object instance)
        {
            return GetAttributes<TAttribute>(instance, false);
        }

        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(object instance, bool inherit)
        {
            Contract.Requires(instance != null);
            return GetAttributes<TAttribute>(instance.GetType(), inherit);
        }

        public static TAttribute GetAttribute<TAttribute>(this ICustomAttributeProvider attributeProvider)
        {
            return GetAttribute<TAttribute>(attributeProvider, false);
        }

        public static TAttribute GetAttribute<TAttribute>(this ICustomAttributeProvider attributeProvider, bool inherit)
        {
            Contract.Requires(attributeProvider != null);

            return GetAttributes<TAttribute>(attributeProvider, inherit).FirstOrDefault();
        }

        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this ICustomAttributeProvider attributeProvider)
        {
            return GetAttributes<TAttribute>(attributeProvider, false);
        }

        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this ICustomAttributeProvider attributeProvider, bool inherit)
        {
            Contract.Requires(attributeProvider != null);
            return attributeProvider.GetCustomAttributes(typeof (TAttribute), inherit).Cast<TAttribute>();
        }

        public static TValue GetAttributeValue<TAttribute, TValue>(this ICustomAttributeProvider attributeProvider, Func<TAttribute, TValue> accessor,
            TValue defaultValue = default(TValue))
            where TAttribute : class
        {
            Contract.Requires(attributeProvider != null);
            Contract.Requires(accessor != null);

            var attribute = attributeProvider.GetAttribute<TAttribute>();
            if (attribute != null)
                return accessor(attribute);

            return defaultValue;
        }

        public static IEnumerable<PropertyInfo> GetEditableProperties(object instance)
        {
            IEnumerable<PropertyInfo> properties = instance.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance)
                .Select(m => m as PropertyInfo)
                .Where(p => p != null && p.CanRead && p.CanWrite);

            return properties;
        }
    }
}