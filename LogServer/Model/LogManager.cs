using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;

namespace LogServer.Model
{
    public class LogManager
    {

        private static ResourceManager _rm = new ResourceManager("LogServer.Resource", Assembly.GetExecutingAssembly());

        public static IEnumerable<string> GetAppNames()
        {
            var appNames = Directory.GetFiles(_rm.GetString("LogFolderPath"), "*.txt").Select(f => Path.GetFileNameWithoutExtension(f));

            //for (int i = 0; i < appNames.Length; i++)
            //{
            //    Console.WriteLine("app" + i + ": " + appNames[i]);
            //}

            return appNames/*.OfType<string>().ToList()*/;
        }

        public static IEnumerable<LogMessage> GetAppLogMessages(string appName)
        {
            List<LogMessage> logs = new List<LogMessage>();

            using (StreamReader r = new StreamReader($"{_rm.GetString("LogFolderPath")}/{appName}.txt"))
            {
                string json = r.ReadLine();
                while (json != null)
                {
                    logs.Add(JsonConvert.DeserializeObject<LogMessage>(json));
                    json = r.ReadLine();
                }
            }

            return logs;
        }

        public static void SaveLogMessage(LogMessage log)
        {
            using (StreamWriter file = new StreamWriter($"{_rm.GetString("LogFolderPath")}/{log.AppName}.txt", true))
            {
                file.WriteLine(JsonConvert.SerializeObject(log));
            }
        }

    }
}
