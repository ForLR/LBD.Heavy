using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace LBD.Infrastructure
{
    public static class ConfigMange
    {
        private static string _AppsettingPath { get; set; } = "appsettings.json";
        private static string AttName { get; set; } = "ConnectionStr";

        public static string ConfigStr { get; private set; }



        static ConfigMange()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(_AppsettingPath).Build();
            ConfigStr = configuration[AttName];

        }

        
    }
}
