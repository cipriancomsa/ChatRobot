using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ApplicationBoot.ServiceModel;
using Utils.Assemblies;

namespace ApplicationBoot
{
    public static class Init
    {
        public static void InitializeApplication()
        {
            RegisterServices();
        }

        private static void RegisterServices()
        {
            try
            {
                Assembly[] assemblies = AssemblyHelper.LoadAssemblies(Directory.GetCurrentDirectory(), "*.dll").ToArray();
                var unityBoostrapper = new ServiceUnityBoostrapper(assemblies);
                unityBoostrapper.Run();
            }
            catch (ReflectionTypeLoadException ex)
            {
                var sb = new StringBuilder();
                foreach (Exception exSub in ex.LoaderExceptions)
                {
                    sb.AppendLine(exSub.Message);
                    var exFileNotFound = exSub as FileNotFoundException;
                    if (exFileNotFound != null)
                    {
                        if (!string.IsNullOrEmpty(exFileNotFound.FusionLog))
                        {
                            sb.AppendLine("Fusion Log:");
                            sb.AppendLine(exFileNotFound.FusionLog);
                        }
                    }
                    sb.AppendLine();
                }
                string errorMessage = sb.ToString();
            }
        }
    }
}
