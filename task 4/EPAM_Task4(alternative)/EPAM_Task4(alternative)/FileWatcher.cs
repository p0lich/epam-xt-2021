using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace EPAM_Task4_alternative_
{
    public class FileWatcher
    {
        private DirectoryInfo RootDirectory { get; }
        private FileSystemWatcher Watcher { get; set; }
        private DirectoryInfo LogFolder { get; set; }
        private DirectoryInfo LogText { get; set; }
        private string InitialFolderName { get; }

        public FileWatcher(string rootPath)
        {
            CreateRootDirectory(rootPath);
            RootDirectory = new DirectoryInfo(rootPath);
            CreateLogFolder(RootDirectory, "ChangeHistory");
            LogTextCreate(LogFolder);
            InitialFolderName = "InitialState";
            SaveInitialState();

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

            return dates;
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

        private void SaveInitialState()
        {
            string folderName = InitialFolderName;
            string folderPath = Path.Combine(LogFolder.FullName, folderName);

            if (Directory.Exists(folderPath))
            {
                return;
            }

            Directory.CreateDirectory(folderPath);

            List<string> filesPaths = GetAllOpenFilePaths(RootDirectory.FullName);
            List<string> copyFilesPaths = new List<string>();

            for (int i = 0; i < filesPaths.Count; i++)
            {
                copyFilesPaths.Add(filesPaths[i].Replace(RootDirectory.Name,
                    $"{RootDirectory.Name}\\{LogFolder.Name}\\{folderName}"));

                string newFolderPath = copyFilesPaths[i].Remove(copyFilesPaths[i].LastIndexOf('\\'));

                Directory.CreateDirectory(newFolderPath);
                File.Copy(filesPaths[i], copyFilesPaths[i]);
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

        public static List<string> GetAllOpenFilePaths(string dir)
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

            LogMaintain(e.FullPath, e.OldFullPath);
        }

        private void WatcherError(object sender, ErrorEventArgs e)
        {
            Exception ex = e.GetException();

            if (ex != null)
            {
                throw new Exception();
            }
        }

        #endregion

        private void LogMaintain(string targetFilePath, WatcherChangeTypes operationType)
        {
            string stringChangeTime = DateTime.Now.ToString("dd.MM.yyyy-HH.mm.ss");

            LogWrite(targetFilePath, operationType, stringChangeTime);

            if (operationType != WatcherChangeTypes.Deleted)
            {
                SaveChanges(targetFilePath, stringChangeTime);
            }
        }

        private void LogMaintain(string targetFilePath, string oldTargetFilePath)
        {
            string stringChangeTime = DateTime.Now.ToString("dd.MM.yyyy-HH.mm.ss");

            LogWrite(targetFilePath, oldTargetFilePath, stringChangeTime);
            SaveChanges(targetFilePath, stringChangeTime);
        }

        private void LogWrite(string targetPath, WatcherChangeTypes operationType, string stringChangeTime)
        {
            using (StreamWriter logFileStream = new StreamWriter(LogText.FullName, true))
            {
                logFileStream.WriteLine($"{stringChangeTime} | {operationType.ToString().ToUpper()} | {targetPath}");
            }
        }

        private void LogWrite(string targetPath, string oldTargetPathstring, string stringChangeTime)
        {
            using (StreamWriter logFileStream = new StreamWriter(LogText.FullName, true))
            {
                logFileStream.WriteLine($"{stringChangeTime} |" +
                    $" {WatcherChangeTypes.Renamed.ToString().ToUpper()} | " +
                    $"FROM: {oldTargetPathstring} <> TO: {targetPath}");
            }
        }

        private void SaveChanges(string targetFilePath, string stringChangeTime)
        {
            if (!File.Exists(targetFilePath))
            {
                throw new FileNotFoundException();
            }

            string logPath = targetFilePath.Replace(RootDirectory.Name, 
                $"{RootDirectory.Name}\\{LogFolder.Name}\\{stringChangeTime}");

            string logFolderPath = logPath.Remove(logPath.LastIndexOf('\\'));

            Directory.CreateDirectory(logFolderPath);

            if (File.Exists(logPath))
            {
                File.Delete(logPath);
            }

            File.Copy(targetFilePath, logPath);
        }

        public void ReturnChanges(int targetIndex)
        {
            List<string[]> logData = GetLogData();

            int currentStep = 0;

            ReturnToInitial();

            while (currentStep < targetIndex)
            {
                string[] currentLog = logData[currentStep];

                DataReturn(currentLog);

                currentStep++;
            }
        }

        private void ReturnToInitial()
        {
            List<string> initialStateFiles = GetAllOpenFilePaths($"{LogFolder.FullName}\\{InitialFolderName}");
            List<string> currentFilesPaths = GetAllOpenFilePaths($"{RootDirectory.FullName}");

            foreach (var path in currentFilesPaths)
            {
                File.Delete(path);
            }

            foreach (var path in initialStateFiles)
            {
                string targetPath = path.Replace($"{LogFolder.Name}\\{InitialFolderName}", "");

                string folderPath = targetPath.Remove(targetPath.LastIndexOf('\\'));

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                File.Copy(path, targetPath);
            }
        }

        private void DataReturn(string[] logData)
        {
            //logData[0] - string data
            //logData[1] - action type
            //logData[2] - path where change was occures

            string actionType = logData[1].Trim();

            if (actionType == "CREATED" || actionType == "CHANGED")
            {
                DataRestore(logData);
                return;
            }

            if (actionType == "DELETED")
            {
                if (File.Exists(logData[2].Trim()))
                {
                    File.Delete(logData[2].Trim());
                }

                return;
            }

            // RENAME case

            // Get clear paths
            string[] paths = logData[2].Split("<>");

            paths[0] = paths[0].Replace("FROM:", "").Trim(); // path with old name
            paths[1] = paths[1].Replace("TO:", "").Trim();   // path with new name

            if (File.Exists(paths[0]))
            {
                File.Delete(paths[0]);
            }

            string logPath = paths[1].Replace(RootDirectory.Name,
                $"{RootDirectory.Name}\\{LogFolder.Name}\\{logData[0].Trim()}");

            File.Copy(logPath, paths[1]);
        }

        private void DataRestore(string[] logData)
        {
            string targetPath = logData[2].Trim();

            string logPath = targetPath.Replace(RootDirectory.Name,
                $"{RootDirectory.Name}\\{LogFolder.Name}\\{logData[0].TrimEnd()}");

            if (File.Exists(targetPath))
            {
                File.Delete(targetPath);
            }

            string targetFileDirectory = targetPath.Remove(targetPath.LastIndexOf('\\'));

            if (!Directory.Exists(targetFileDirectory))
            {
                Directory.CreateDirectory(targetFileDirectory);
            }

            File.Copy(logPath, targetPath);
        }
    }
}
