using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RDSync.Engines
{
    /// <summary>
    /// Application Configuration
    /// </summary>
    [Serializable]
    public class AppConfig
    {
        /// <summary>
        /// Path of AppDataFile stored
        /// </summary>
        private static string dataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                + Path.DirectorySeparatorChar + "RDSync";
        /// <summary>
        /// configuration file name
        /// </summary>
        private static string fileName = "config.xml";
        /// <summary>
        /// Date Format for Direcotry Name
        /// </summary>
        private string dateFormat = "yyyy-MM-dd";
        /// <summary>
        /// Target Media Config List
        /// </summary>
        private List<MediaConfig> mediaConfigList;

        public const string NonSkippedFile = "skippedfile_{0}.csv";

        public string DateFormat { get => dateFormat; set => dateFormat = value; }
        public List<MediaConfig> MediaConfigList { get => mediaConfigList; set => mediaConfigList = value; }
        public static string DataPath { get => dataPath; }
        public static string FileName { get => fileName; }

        /// <summary>
        /// To Get Application Config
        /// </summary>
        /// <returns></returns>
        public static AppConfig GetAppConfig()
        {
            // load configuration file
            AppConfig appConfig = appConfig = Load();
            if (appConfig == null)
            {
                // if configuration file is not found create deafault data.
                appConfig = CreateDefaultConfig();
            }
            return appConfig;
        }
        /// <summary>
        /// Load configuration file
        /// </summary>
        /// <returns></returns>
        private static AppConfig Load()
        {
            // if there isn't configuratino file return null
            FileInfo file = ExistConfigFile();
            if (file == null)
                return null;
            // Deserialize configuration file
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.
                    XmlSerializer(typeof(AppConfig));
            StreamReader stream = new StreamReader(file.FullName, new System.Text.UTF8Encoding(false));
            AppConfig appConfig = (AppConfig)xmlSerializer.Deserialize(stream);
            stream.Close();
            return appConfig;
        }

        public static FileInfo ExistConfigFile()
        {
            FileInfo file = new FileInfo(GetConfigFilePath());
            if (!file.Exists)
                return null;
            return file;
        }
        /// <summary>
        /// Create Default Config Data
        /// </summary>
        /// <returns></returns>
        private static AppConfig CreateDefaultConfig()
        {
            // Create deafault configuraion data
            AppConfig appConfig = new AppConfig {MediaConfigList = MediaConfig.CreateDefaultConfig()};
            return appConfig;
        }
        /// <summary>
        /// Store configuration file
        /// </summary>
        /// <param name="appConfig"></param>
        public static void Store(AppConfig appConfig)
        {
            // Confirm if data directory exists
            DirectoryInfo directory = new DirectoryInfo(DataPath);
            // if directory does not exist it is created
            if (!directory.Exists)
                directory.Create();
            // Serialize configuration data
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.
                    XmlSerializer(typeof(AppConfig));
            StreamWriter stream = new StreamWriter(GetConfigFilePath(), false, new System.Text.UTF8Encoding(false));
            xmlSerializer.Serialize(stream, appConfig);
            stream.Close();
        }
        /// <summary>
        /// Get configuration file path
        /// </summary>
        /// <returns></returns>
        public static string GetConfigFilePath()
        {
            return DataPath + Path.DirectorySeparatorChar + FileName;
        }
    }
}
