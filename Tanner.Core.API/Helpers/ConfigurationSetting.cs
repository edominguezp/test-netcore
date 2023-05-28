using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Tanner.Core.API.Helpers
{
    /// <summary>
    /// Class that represent the configuration settings
    /// </summary>
    public class ConfigurationSetting
    {
        /// <summary>
        /// Instance for settings
        /// </summary>
        public static readonly ConfigurationSetting instance = new ConfigurationSetting();


        /// <summary>
        /// Connection string
        /// </summary>
        public readonly string _connectionString = string.Empty;

        private IConfigurationRoot configurationRoot;

        /// <summary>
        /// Configuration settings for builder
        /// </summary>
        public ConfigurationSetting()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Settings/appsettings.json");
            configurationBuilder.AddJsonFile(path, false);



            configurationRoot = configurationBuilder.Build();
        }


        /// <summary>
        /// Get the key for setting
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetValue(string key)
        {
            var output = configurationRoot.GetValue<string>(key);



            if (string.IsNullOrEmpty(output))
                throw new Exception($"No se encontró valor para la key [{key}]");



            return output;
        }


        /// <summary>
        /// Get values for settings
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetValueAs<T>(string key)
        {
            var output = configurationRoot.GetValue<T>(key);
            return output;
        }
    }
}
