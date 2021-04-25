using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EPAM_Task4._1
{
    public class FileWatcher
    {
        private DirectoryInfo RootDirectory { get; }
        private FileSystemWatcher Watcher { get; set; }
        private DirectoryInfo LogFolder { get; set; }
        private DirectoryInfo LogText { get; set; }

        public FileWatcher(string rootPath)
        {
            CreateRootDirectory(rootPath);
            RootDirectory = new DirectoryInfo(rootPath);
            CreateLogFolder(RootDirectory, "ChangeHistory");
            LogTextCreate(LogFolder);

            Watcher = new FileSystemWatcher(rootPath);

            Watcher.Changed += WathcerChanged;
            Watcher.Created += WathcerCreated;
            Watcher.Deleted += WathcerDeleted;
            Watcher.Renamed += WathcerRenamed;
            Watcher.Error += WatcherError;

            Watcher.Filter = "*.txt";
            Watcher.IncludeSubdirectories = true;
        }

        public void StartWatching()
        {
            Watcher.EnableRaisingEvents = true;
        }

        public void EndWatching()
        {
            Watcher.EnableRaisingEvents = false;         
        }

        public List<string> GetChangeDates()
        {
            List<string> dates = new List<string>();
            List<string[]> logData = GetLogData();

            for (int i = 0; i < logData.Count; i++)
            {
                dates.Add(logData[i][0]);
            }

            return dates.Distinct().ToList();
        }

        private List<string[]> GetLogData()
        {
            using (StreamReader sr = new StreamReader(LogText.FullName))
            {
                string line;

                List<string[]> separatedLogData = new List<string[]>();

                while ((line = sr.ReadLine()) != null)
                {
                    string[] splitLogInfo = line.Split('|');
                    separatedLogData.Add(splitLogInfo);
                }

                return separatedLogData;
            }
        }

        #region Creation of neccecary folders and texts files

        private void CreateRootDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private void CreateLogFolder(DirectoryInfo directoryInfo, string folderName)
        {
            string logFolderPath = Path.Combine(directoryInfo.FullName, folderName);

            if (Directory.Exists(logFolderPath))
            {
                LogFolder = new DirectoryInfo(logFolderPath);
                return;
            }

            LogFolder = Directory.CreateDirectory(logFolderPath);
            LogFolder.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
        }

        private void LogTextCreate(DirectoryInfo logFolderPath)
        {
            string logTextPath = Path.Combine(logFolderPath.FullName, "ChangeHistory.txt");

            if (File.Exists(logTextPath))
            {
                LogText = new DirectoryInfo(logTextPath);
                return;
            }

            using (FileStream fs = File.Create(logTextPath))
            {
                File.SetAttributes(logTextPath, FileAttributes.Hidden);
            }   

            LogText = new DirectoryInfo(logTextPath);
        }

        #endregion

        private static List<string> GetAllOpenFilePaths(string dir)
        {
            List<string> filesList = new List<string>();

            string[] allDirs = Directory.GetDirectories(dir);
            List<string> normalDirs = new List<string>();

            foreach (var folderPath in allDirs)
            {
                DirectoryInfo folderInfo = new DirectoryInfo(folderPath);

                if (folderInfo.Attributes != (FileAttributes.Directory | FileAttributes.Hidden))
                {
                    normalDirs.Add(folderPath);
                }
            }

            filesList.AddRange(Directory.GetFiles(dir));

            foreach (var subdirectory in normalDirs)
            {
                try
                {
                    filesList.AddRange(GetAllOpenFilePaths(subdirectory));
                }

                catch (DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }

            return filesList;
        }

        #region Watcher events

        private void WathcerChanged(object sender, FileSystemEventArgs e)
        {
            if (e.FullPath.Contains(LogFolder.Name))
            {
                return;
            }

            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }

            LogMaintain(e.FullPath, e.ChangeType);
        }

        private void WathcerCreated(object sender, FileSystemEventArgs e)
        {
            if (e.FullPath.Contains(LogFolder.Name))
            {
                return;
            }

            LogMaintain(e.FullPath, e.ChangeType);
        }

        private void WathcerDeleted(object sender, FileSystemEventArgs e)
        {
            if (e.FullPath.Contains(LogFolder.Name))
            {
                return;
            }

            LogMaintain(e.FullPath, e.ChangeType);
        }

        private void WathcerRenamed(object sender, RenamedEventArgs e)
        {
            if (e.FullPath.Contains(LogFolder.Name))
            {
                return;
            }

            LogMaintain(e.FullPath, e.ChangeType);
        }

        private void WatcherError(object sender, ErrorEventArgs e)
        {
            Exception ex = e.GetException();

            if (ex != null)
            {
                throw ex;
            }
        }

        #endregion

        private void LogMaintain(string targetFilePath, WatcherChangeTypes operationType)
        {
            string stringChangeTime = DateTime.Now.ToString("dd.MM.yyyy-HH.mm.ss");

            LogWrite(targetFilePath, operationType, stringChangeTime);

            SaveCurrentFolder(stringChangeTime);
        }

        private void LogWrite(string targetPath, WatcherChangeTypes operationType, string stringChangeTime)
        {
            using (StreamWriter logFileStream = new StreamWriter(LogText.FullName, true))
            {
                logFileStream.WriteLine($"{stringChangeTime} | {operationType.ToString().ToUpper()} | {targetPath}");
            }
        }

        private void SaveCurrentFolder(string stringChangeTime)
        {
            if (Directory.Exists($"{LogFolder.FullName}\\{stringChangeTime}"))
            {
                Directory.Delete($"{LogFolder.FullName}\\{stringChangeTime}", true);
            }

            List<string> filePaths = GetAllOpenFilePaths(RootDirectory.FullName);

            if (filePaths.Count == 0)
            {
                Directory.CreateDirectory($"{LogFolder.FullName}\\{stringChangeTime}");
                return;
            }

            foreach (var path in filePaths)
            {
                string logPath = path.Replace(RootDirectory.Name,
                    $"{RootDirectory.Name}\\{LogFolder.Name}\\{stringChangeTime}");

                string folderPath = logPath.Remove(logPath.LastIndexOf('\\'));

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                if (!File.Exists(logPath) && File.Exists(path))
                {
                    File.Copy(path, logPath);
                }
            }
        }

        public void ReturnChanges(int targetIndex)
        {
            List<string> uniqueDates = GetChangeDates();

            string changeData = uniqueDates[targetIndex - 1].Trim();

            List<string> allLogPaths = GetAllOpenFilePaths($"{LogFolder.FullName}\\{changeData}");
            List<string> currentFilesPath = GetAllOpenFilePaths(RootDirectory.FullName);

            foreach (var path in currentFilesPath)
            {
                File.Delete(path);
            }

            foreach (var path in allLogPaths)
            {
                string targetPath = path.Replace($"{LogFolder.Name}\\{changeData}\\", "");

                string folderPath = targetPath.Remove(targetPath.LastIndexOf('\\'));

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                File.Copy(path, targetPath);
            }
        }
    }
}
