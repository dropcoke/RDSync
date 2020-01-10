using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDSync.Engines;

namespace RDSync
{
    class ConfigData
    {
        private static AppConfig appConfig;

        private static Dictionary<MediaType, MediaConfig> mediaTypeList = new Dictionary<MediaType, MediaConfig>();

        public static AppConfig AppConfig { get => appConfig;}
        public static Dictionary<MediaType, MediaConfig> MediaTypeList { get => mediaTypeList; }

        

        private static void SetMediaRootDirectory()
        {
            foreach (MediaConfig mediaConfig in appConfig.MediaConfigList)
            {
                mediaTypeList.Add(mediaConfig.Media, mediaConfig);
            }
        }

        public static void Init()
        {
            appConfig = AppConfig.GetAppConfig();
            SetMediaRootDirectory();
        }
    }
}
