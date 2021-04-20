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
            RootDirectory = new DirectoryInfo(rootPath);
            CreateLogFolder(RootDirectory, "ChangeHistory");
            LogTextCreate(LogFolder);
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

        private void SaveInitialState()
        {
            string folderName = "Initial state";

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

            File.Create(logTextPath);
            File.SetAttributes(logTextPath, FileAttributes.Hidden);

            LogText = new DirectoryInfo(logTextPath);
        }

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
            Console.WriteLine("This file was changed: " + e.FullPath);
        }

        private void WathcerCreated(object sender, FileSystemEventArgs e)
        {
            if (e.FullPath.Contains(LogFolder.Name))
            {
                return;
            }

            LogMaintain(e.FullPath, e.ChangeType);
            Console.WriteLine("Created new txt file on directory: " + e.FullPath);
        }

        private void WathcerDeleted(object sender, FileSystemEventArgs e)
        {
            if (e.FullPath.Contains(LogFolder.Name))
            {
                return;
            }

            LogMaintain(e.FullPath, e.ChangeType);
            Console.WriteLine("File on this direcroty was deleted: " + e.FullPath);
        }

        private void WathcerRenamed(object sender, RenamedEventArgs e)
        {
            if (e.FullPath.Contains(LogFolder.Name))
            {
                return;
            }

            LogMaintain(e.FullPath, e.ChangeType);
            Console.WriteLine("Rename:");
            Console.WriteLine("From: " + e.OldFullPath);
            Console.WriteLine("To: " + e.FullPath);
        }

        private void WatcherError(object sender, ErrorEventArgs e)
        {
            Exception ex = e.GetException();

            if (ex != null)
            {
                Console.WriteLine("Message: " + ex.Message);
                Console.WriteLine("Stacktrace:");
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine();
            }
        }

        private void LogMaintain(string targetFilePath, WatcherChangeTypes operationType)
        {
            string stringChangeTime = DateTime.Now.ToString("dd.MM.yyyy-HH.mm.ss");

            LogWrite(targetFilePath, operationType, stringChangeTime);

            if (operationType != WatcherChangeTypes.Deleted)
            {
                SaveChanges(targetFilePath, stringChangeTime);
            }
        }

        private void LogWrite(string targetPath, WatcherChangeTypes operationType, string stringChangeTime)
        {
            using (StreamWriter logFileStream = new StreamWriter(LogText.FullName, true))
            {
                logFileStream.WriteLine($"{stringChangeTime} | {operationType.ToString().ToUpper()} | {targetPath}");
            }
        }

        private void SaveChanges(string targetFilePath, string stringChangeTime)
        {
            if (!File.Exists(targetFilePath))
            {
                throw new FileNotFoundException();
            }
            
            DirectoryInfo logPath = new DirectoryInfo(targetFilePath.Replace(RootDirectory.Name,
                $"{RootDirectory.Name}\\{LogFolder.Name}\\{stringChangeTime}"));

            string logFolderPath = logPath.FullName.Remove(logPath.FullName.LastIndexOf('\\'));

            Directory.CreateDirectory(logFolderPath);
            File.Copy(targetFilePath, logPath.FullName);
        }

        private void ReturnChanges(int targetIndex)
        {
            List<string[]> logData = GetLogData();

            int returnCount = logData.Count - targetIndex;
            int currentStep = 0;

            while (currentStep != returnCount)
            {
                string[] currentLogData = logData[logData.Count - currentStep - 1];
                string[] oldLogData = logData[logData.Count - currentStep - 2];

                for (int i = 0; i < currentLogData.Length; i++)
                {
                    currentLogData[i] = currentLogData[i].Trim();
                }

                DataReturn(currentLogData, oldLogData);
                currentStep++;
            }
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

        private void DataReturn(string[] currentLogData, string[] oldLogData)
        {
            //logData[0] - string data
            //logData[1] - action type
            //logData[2] - path where cahnge was occures

            switch (currentLogData[1])
            {
                case "CHANGED":
                    //TODO: restore data before changing
                    break;

                case "CREATED":
                    //TODO: delete data
                    break;

                case "RENAMED":
                    //TODO: restore old name
                    break;

                case "DELETED":
                    //TODO: create file
                    break;

                default:
                    break;
            }
        }

        private void DataRestore()
        {

        }

        private void DataDelete()
        {

        }
    }
}
