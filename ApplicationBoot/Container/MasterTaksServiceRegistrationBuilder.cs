#region

#endregion

namespace ApplicationBoot.Container
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using ApplicationBoot.Contracts;
    using Microsoft.Practices.ServiceLocation;

    public class MasterTaksServiceRegistrationBuilder : IRegistrationBuilder
    {
        public void Register()
        {
            var allTaks = new List<IModule>();
            if (ServiceLocator.Current == null)
            {
                throw new Exception("Service Locator error");
            }
           
            if (!allTaks.Any())
            {
                var singleTask = ServiceLocator.Current.GetInstance<IModule>();
                allTaks.Add(singleTask);
            }

            foreach (IModule task in allTaks)
            {
                Debug.WriteLine("Before init " + task);
                task.Init();
                Debug.WriteLine("After init " + task);
            }
        }

        public static string GetCurrentExecutingDirectory(Assembly assembly)
        {
            string filePath = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            return Path.GetDirectoryName(filePath);
        }
    }
}