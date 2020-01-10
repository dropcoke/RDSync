using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.IO;
using System.Threading;
using System.Linq;

namespace RDSync.Engines
{
    /// <summary>
    /// File Sync Core Class
    /// </summary>
    public class FileTransfer
    {
        /// <summary>
        /// application config
        /// </summary>
        private AppConfig appConfig;
        /// <summary>
        /// count of copied files
        /// </summary>
        private int fileCount = 0;
        /// <summary>
        /// size of copied files
        /// </summary>
        private long totalSize = 0;
        /// <summary>
        /// target files to copy to local disc
        /// </summary>
        private List<FileInfo> copyFileList = new List<FileInfo>();

        public AppConfig AppConfig { get => appConfig; set => appConfig = value; }
        public int FileCount { get => fileCount; set => fileCount = value; }
        public long TotalSize { get => totalSize; set => totalSize = value; }
        public List<FileInfo> CopyFileList { get => copyFileList; set => copyFileList = value; }

        /// <summary>
        /// Initialize
        /// Search files which are not copied yet.
        /// </summary>
        public void Init()
        {
            // Load configuration file
            FileInfo file = AppConfig.ExistConfigFile();
            if (file == null)
            {
                // if configuration file does not exists, exit.
                throw new Exception("Require application setting");
            }
            // Get configuration data
            this.appConfig = AppConfig.GetAppConfig();
            // Get drives
            var drives = DriveInfo.GetDrives();
            foreach (var drive in drives)
            {
                // Get removable drives
                if (drive.DriveType == DriveType.Removable)
                {
                    DirectoryInfo directory = new DirectoryInfo(drive.Name);
                    this.GetDirectory(directory);
                }
            }
        }
        /// <summary>
        /// Get all directory of removable storage
        /// </summary>
        /// <param name="directory"></param>
        private void GetDirectory(DirectoryInfo directory)
        {
            // get child directory
            var directories = directory.GetDirectories();
            // search media files which don't exist on local disc.
            this.Calculate(directory);
            // if directory has children, recurse this method
            if (directories.Length > 0)
            {
                foreach (var childDirectory in directories)
                {
                    this.GetDirectory(childDirectory);
                }
            }
        }
        /// <summary>
        /// Get new files and calculate file count and total size
        /// </summary>
        /// <param name="directory"></param>
        private void Calculate(DirectoryInfo directory)
        {
            // Get files of current directory
            var files = directory.GetFiles();
            foreach (var file in files)
            {
                // confirm media type
                MediaConfig mediaConfig = this.GetMediaType(file);
                // if media type is not declared skip to next
                if (mediaConfig == null)
                    continue;
                // Create path of destination
                string path = this.GetDestinationDirectory(mediaConfig, file);
                this.CreateDestinationDirectory(path);
                // confirm if same file exists
                if (this.hasSameFile(path, file))
                    continue;
            }
        }
        /// <summary>
        /// get Media Type (like Image, Video, Audio or etc...
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public MediaConfig GetMediaType(FileInfo file) 
        {
            // if file name starts from "." exit
            if (file.Name.Substring(0, 1).Equals("."))
                return null;
            // repeat all of media types which is declared on configuration
            foreach (MediaConfig mediaConfig in this.appConfig.MediaConfigList)
            {
                // confirm file extension
                foreach (string target in mediaConfig.Extensions)
                {
                    if (file.Extension.ToUpper().Equals(target.ToUpper()))
                        return mediaConfig;
                }
            }
            return null;

        }
        /// <summary>
        /// confirm if same file exists
        /// </summary>
        /// <param name="path"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool hasSameFile(string path, FileInfo file)
        {
            // create target file path
            path += Path.DirectorySeparatorChar + file.Name;
            FileInfo target = new FileInfo(path);
            // if same file exists, exit
            if (target.Exists)
            {
                return true;
            }
            this.FileCount++;
            this.TotalSize += file.Length;
            this.CopyFileList.Add(file);
            return false;
        }
        /// <summary>
        /// Create destination directory of local disc
        /// </summary>
        /// <param name="path"></param>
        public void CreateDestinationDirectory(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            if (directory.Exists)
                return;
            directory.Create();

        }
        /// <summary>
        /// Get destination directory by media type
        /// </summary>
        /// <param name="mediaConfig"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public string GetDestinationDirectory(MediaConfig mediaConfig, FileInfo file)
        {
            string creationDate = file.CreationTime.ToString(this.appConfig.DateFormat);
            DirectoryInfo directory = new DirectoryInfo(mediaConfig.MediaRootPath);
            if (!directory.Exists)
                directory.Create();
            return mediaConfig.MediaRootPath + Path.DirectorySeparatorChar + creationDate;
        }

    }

    

}
