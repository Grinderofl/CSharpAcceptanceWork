using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAcceptanceWork
{
    public static class ConfigurationReader
    {
        private static readonly ExeConfigurationFileMap Map;

        private static System.Configuration.Configuration _configuration;

        static ConfigurationReader()
        {
            Map = new ExeConfigurationFileMap {ExeConfigFilename = "AppConfig.config"};
        }

        static void ReloadConfiguration()
        {
            _configuration = File.Exists(Map.ExeConfigFilename) ? ConfigurationManager.OpenMappedExeConfiguration(Map, ConfigurationUserLevel.None) : null;
        }

        public static T ReadConfigurationEntry<T>(string key)
        {
            if (_configuration != null && _configuration.AppSettings.Settings[key] != null)
                return (T) Convert.ChangeType(_configuration.AppSettings.Settings[key], typeof (T));
            
            // Return from defaultsettings
            throw new NotImplementedException();
        }
    }
}
