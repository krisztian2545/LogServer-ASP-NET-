using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LogServer
{
    public class Program
    {

        private const string _urlPath = "url.txt";

        public static void Main(string[] args)
        {
            Console.WriteLine("starting...");
            CreateNecessaryFiles();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls(GetUrl());
                });

        public static String GetUrl()
        {
            string url;
            using (StreamReader r = new StreamReader(_urlPath))
            {
                url = r.ReadLine();
            }

            return url;
        }

        public static void CreateNecessaryFiles()
        {
            // creating url file if doesn't exist
            //using (StreamWriter file = new StreamWriter(_urlPath, true))
            //{
            //    file.WriteLine(JsonConvert.SerializeObject(log));
            //}

            // create directory for logs if doesn't exist
            Directory.CreateDirectory( (new ResourceManager("LogServer.Resource", Assembly.GetExecutingAssembly())).GetString("LogFolderPath") );
        }

    }
}
