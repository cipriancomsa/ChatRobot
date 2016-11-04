using System.Net.Mime;

namespace Utils.Assemblies
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    public static class AssemblyHelper
    {
        public static IEnumerable<Assembly> LoadAssemblies(string path, string fileFilter)
        {
            var test1 = Directory.GetParent(Directory.GetCurrentDirectory()).Parent + "/SharedAssemblies";
            string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            
            
            IEnumerable<Assembly> assemblies =
                Directory.EnumerateFiles(path, fileFilter)
                    .Select(file => Assembly.ReflectionOnlyLoadFrom(file).FullName)
                    .Select(Assembly.Load);
            return assemblies;
        }

        public static T LoadAppSetting<T>(string sectionName, string key)
        {
            var section = ConfigurationManager.GetSection(sectionName) as NameValueCollection;
            object value = section[key];
            return (T) Convert.ChangeType(value, typeof (T));
        }

        public static IList<string> GetSectionNames()
        {
            Configuration cfgHandler = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            IEnumerable<ConfigurationSection> localSections = cfgHandler.Sections.Cast<ConfigurationSection>().Where(s => s.SectionInformation.IsDeclared);
            return localSections.Select(sectionName => sectionName.SectionInformation.SectionName).ToList();
        }
    }
}