using System;
using System.Collections.Generic;
using System.IO;

namespace RDSync.Engines
{
    [Serializable]
    public class MediaConfig
    {
        /// <summary>
        /// Default extensions
        /// </summary>
        private static string[] defaultExtensionImage = { ".jpg", ".png", ".gif", ".raw" };
        private static string[] defaultExtensionVideo = { ".mov", ".mp4", ".avi", ".mpg", ".wmv", ".flv", ".mts", ".m2ts" };
        private static string[] defaultExtensionAudio = { ".mp3", ".wav", ".aiff", ".aac", "aif", ".m4a" };
        private static string[] defaultExtensionExcel = { ".xlsx", ".xls" };
        private static string[] defaultExtensionWord = { ".docs", ".doc" };
        private static string[] defaultExtensionPdf = { ".pdf" };
        private static string[] defaultExtensionHtml = { ".html" };
        private static string[] defaultExtensionCsv = { ".csv" };
        /// <summary>
        /// Default path of destination
        /// </summary>
        private static string defaultDestination = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + "file_transfer";
        /// <summary>
        /// Default target medias
        /// </summary>
        private static MediaType[] defaultMediaType = { MediaType.Image, MediaType.Video, MediaType.Audio };
        // All of target medias
        private static MediaType[] fullMediaType = { MediaType.Image, MediaType.Video, MediaType.Audio,
                                MediaType.Excel, MediaType.Word, MediaType.PDF, MediaType.CSV, MediaType.HTML };
        /// <summary>
        /// target media type
        /// </summary>
        private MediaType media;
        /// <summary>
        /// permitted file extension list
        /// </summary>
        private List<string> extensions = new List<string>();
        /// <summary>
        /// root directory where copied file is saved
        /// </summary>
        private string mediaRootPath = "";
        /// <summary>
        /// avalable media type or no
        /// </summary>
        private bool target;

        public MediaType Media { get => media; set => media = value; }
        public List<string> Extensions { get => extensions; set => extensions = value; }
        public string MediaRootPath { get => mediaRootPath; set => mediaRootPath = value; }
        public bool Target { get => target; set => target = value; }
        public static string[] DefaultExtensionImage { get => defaultExtensionImage; set => defaultExtensionImage = value; }
        public static string[] DefaultExtensionVideo { get => defaultExtensionVideo; set => defaultExtensionVideo = value; }
        public static string[] DefaultExtensionAudio { get => defaultExtensionAudio; set => defaultExtensionAudio = value; }
        public static string[] DefaultExtensionExcel { get => defaultExtensionExcel; set => defaultExtensionExcel = value; }
        public static string[] DefaultExtensionWord { get => defaultExtensionWord; set => defaultExtensionWord = value; }
        public static string[] DefaultExtensionPdf { get => defaultExtensionPdf; set => defaultExtensionPdf = value; }
        public static string[] DefaultExtensionHtml { get => defaultExtensionHtml; set => defaultExtensionHtml = value; }
        public static string[] DefaultExtensionCsv { get => defaultExtensionCsv; set => defaultExtensionCsv = value; }
        public static string DefaultDestination { get => defaultDestination; set => defaultDestination = value; }
        public static MediaType[] DefaultMediaType { get => defaultMediaType; set => defaultMediaType = value; }
        public static MediaType[] FullMediaType { get => fullMediaType; set => fullMediaType = value; }

        /// <summary>
        /// Get media type name 
        /// </summary>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        public static string getMediaTypeName(MediaType mediaType)
        {
            switch (mediaType)
            {
                case MediaType.Image:
                    return "Photo";
                case MediaType.Video:
                    return "Video";
                case MediaType.Audio:
                    return "Audio";
                case MediaType.Excel:
                    return "Excel";
            }
            return "Other";
        }
        /// <summary>
        /// Creat Deafult Config Data
        /// </summary>
        public static List<MediaConfig> CreateDefaultConfig()
        {
            List<MediaConfig> list = new List<MediaConfig>();
            foreach (MediaType mediaType in MediaConfig.DefaultMediaType)
            {
                MediaConfig mediaConfig = new MediaConfig { Media = mediaType, 
                            MediaRootPath = GetDefaultDestination(mediaType),
                            Extensions = GetDefaultExtension(mediaType)};
                list.Add(mediaConfig);
            }
            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        private static string GetDefaultDestination(MediaType mediaType)
        {
            return MediaConfig.DefaultDestination + Path.DirectorySeparatorChar + MediaConfig.getMediaTypeName(mediaType);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        private static List<string> GetDefaultExtension(MediaType mediaType)
        {
            List<string> extension = new List<string>();
            switch (mediaType)
            {
                case MediaType.Image:
                    extension.AddRange(MediaConfig.DefaultExtensionImage);
                    break;
                case MediaType.Video:
                    extension.AddRange(MediaConfig.DefaultExtensionVideo);
                    break;
                case MediaType.Audio:
                    extension.AddRange(MediaConfig.DefaultExtensionAudio);
                    break;
            }
            return extension;
        }
    }
    
    public enum MediaType
    {
        Video, Image, Audio, Excel, Word, HTML, PDF, CSV, Other
    }


}
