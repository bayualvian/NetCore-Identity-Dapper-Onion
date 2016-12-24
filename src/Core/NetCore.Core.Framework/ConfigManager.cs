using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NetCore.Core.Entities.ConfigManager;

namespace NetCore.Core.Framework
{
    public class ConfigManager
    {
        private static readonly ConfigManager Instance = new ConfigManager();
        private ConfigEntity _config;
        private bool _isInitialized;

        private ConfigManager()
        {

        }

        public static ConfigManager GetInstance()
        {
            return Instance;
        }

        public void Init(string directoryPath)
        {
            if (!_isInitialized)
            {
                _config = LoadConfigFileToDictionary(directoryPath);
                _isInitialized = true;
            }
        }

        public ConfigEntity GetConfig()
        {
            return _config;
        }

        private ConfigEntity LoadConfigFileToDictionary(String filePath)
        {
            var configDoc = LoadConfigurationFile(filePath);
            return ConfigToDictionary(configDoc);
        }


        private string LoadConfigurationFile(string filePath)
        {
            string Json = System.IO.File.ReadAllText(filePath);
            return Json;
        }

        private ConfigEntity ConfigToDictionary(string json)
        {
            return JsonConvert.DeserializeObject<ConfigEntity>(json);
        }
    }
}
