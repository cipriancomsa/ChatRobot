using ChatRobot.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChatRobot
{
    public static class FileUtils
    {
        public static string Read()
        {
            return File.ReadAllText(DialogFilePath());
        }

        public static string GetBinPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        public static void Add(string text)
        {
            File.AppendAllText(DialogFilePath(), Environment.NewLine + text + Environment.NewLine);
        }

        private static string DialogFilePath()
        {
            return Path.Combine(GetBinPath(), Settings.Default.ResponsesFileName);
        }
    }
}