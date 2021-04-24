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
        private DirectoryInfo TempFolder { get; }
        private DirectoryInfo BackupFolder { get; set; }

        public FileWatcher(string rootPath)
        {
            RootDirectory = new DirectoryInfo(rootPath);
            CreateLogFolder(RootDirectory, "ChangeHistory");
            LogTextCreate(LogFolder);
            SaveInitialState();
            TempFolder = new DirectoryInfo(Path.Combine(LogFolder.FullName, "TempData"));
            //CreateBackupFolder(RootDirectory, "Backup");

            Watcher = new FileSystemWatcher(rootPath);

            Watcher.Changed += WathcerChanged;
            Watcher.Created += WathcerCreated;
            Watcher.Deleted += WathcerDeleted;
            Watcher.Renamed += WathcerRenamed;
            //Watcher.Error += WatcherError;

            Watcher.Filter = "*.txt";
            Watcher.IncludeSubdirectories = true;
        }

        public void StartWatching()
        {
            Watcher.EnableRaisingEvents = true;
            List<string> openFilePaths = GetAllOpenFilePaths(RootDirectory.FullName);
            ManageTemporaryFolder();
        }

        public void EndWatching()
        {
            Watcher.EnableRaisingEvents = false;

            if (Directory.Exists(TempFolder.FullName))
            {
                Directory.Delete(TempFolder.FullName, true);
            }            
        }

        public List<string> GetLogDates()
        {
            List<string> dates = new List<string>();
            List<string[]> logData = GetLogData();

            for (int i = 0; i < logData.Count; i++)
            {
                dates.Add(logData[i][0]);
            }

            return dates;
        }

        private void ManageTemporaryFolder()
        {
            string tempPath = TempFolder.FullName;

            if (!Directory.Exists(tempPath))
            {
                Directory.CreateDirectory(tempPath);
            }

            List<string> filesPaths = GetAllOpenFilePaths(RootDirectory.FullName);
            List<string> copyFilesPaths = new List<string>();

            for (int i = 0; i < filesPaths.Count; i++)
            {
                string fileName = filesPaths[i].Remove(0, filesPaths[i].LastIndexOf('\\'));

                copyFilesPaths.Add(tempPath + fileName);
                File.Copy(filesPaths[i], copyFilesPaths[i]);
            }
        }

        private void ManageTemporaryFolder(string filePath, WatcherChangeTypes type, string oldFileNamePath = null)
        {
            string fileName = filePath.Remove(0, filePath.LastIndexOf('\\'));
            string oldFileName = null;

            if (!String.IsNullOrEmpty(oldFileNamePath))
            {
                oldFileName = oldFileNamePath.Remove(0, filePath.LastIndexOf('\\'));
            }

            switch (type)
            {
                case WatcherChangeTypes.Created:
                    File.Copy(filePath, $"{LogFolder.FullName}\\{TempFolder.Name}{fileName}");
                    return;

                case WatcherChangeTypes.Deleted:
                    File.Delete($"{LogFolder.FullName}\\{TempFolder.Name}{fileName}");
                    return;

                case WatcherChangeTypes.Changed:
                    File.Delete($"{LogFolder.FullName}\\{TempFolder.Name}{fileName}");
                    File.Copy(filePath, $"{LogFolder.FullName}\\{TempFolder.Name}{fileName}");
                    return;

                case WatcherChangeTypes.Renamed:
                    File.Delete($"{LogFolder.FullName}\\{TempFolder.Name}{oldFileName}");
                    File.Copy(filePath, $"{LogFolder.FullName}\\{TempFolder.Name}{fileName}");
                    return;

                default:
                    throw new Exception();
                    return;
            }
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

        private void CreateBackupFolder(DirectoryInfo directoryInfo, string folderName)
        {
            string backupFolderPath = Path.Combine(directoryInfo.FullName, LogFolder.Name, folderName);

            if (Directory.Exists(backupFolderPath))
            {
                LogFolder = new DirectoryInfo(backupFolderPath);
                return;
            }

            LogFolder = Directory.CreateDirectory(backupFolderPath);
            LogFolder.Attributes = FileAttributes.Directory;
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
            ManageTemporaryFolder(e.FullPath, e.ChangeType);

            Console.WriteLine("This file was changed: " + e.FullPath);
        }

        private void WathcerCreated(object sender, FileSystemEventArgs e)
        {
            if (e.FullPath.Contains(LogFolder.Name))
            {
                return;
            }

            LogMaintain(e.FullPath, e.ChangeType);
            ManageTemporaryFolder(e.FullPath, e.ChangeType);

            Console.WriteLine("Created new txt file on directory: " + e.FullPath);
        }

        private void WathcerDeleted(object sender, FileSystemEventArgs e)
        {
            if (e.FullPath.Contains(LogFolder.Name))
            {
                return;
            }

            LogMaintain(e.FullPath, e.ChangeType);
            ManageTemporaryFolder(e.FullPath, e.ChangeType);

            Console.WriteLine("File on this direcroty was deleted: " + e.FullPath);
        }

        private void WathcerRenamed(object sender, RenamedEventArgs e)
        {
            if (e.FullPath.Contains(LogFolder.Name))
            {
                return;
            }

            //LogMaintain(e.FullPath, e.ChangeType);
            LogMaintain(e.FullPath, e.OldFullPath);
            ManageTemporaryFolder(e.FullPath, e.ChangeType, e.OldFullPath);

            Console.WriteLine("Rename:");
            Console.WriteLine("From: " + e.OldFullPath);
            Console.WriteLine("To: " + e.FullPath);
        }

        //private void WatcherError(object sender, ErrorEventArgs e)
        //{
        //    Exception ex = e.GetException();

        //    if (ex != null)
        //    {
        //        Console.WriteLine("Message: " + ex.Message);
        //        Console.WriteLine("Stacktrace:");
        //        Console.WriteLine(ex.StackTrace);
        //        Console.WriteLine();
        //    }
        //}

        private void LogMaintain(string targetFilePath, WatcherChangeTypes operationType)
        {
            string stringChangeTime = DateTime.Now.ToString("dd.MM.yyyy-HH.mm.ss");

            LogWrite(targetFilePath, operationType, stringChangeTime);

            //if (operationType != WatcherChangeTypes.Deleted)
            //{
            //    SaveChanges(targetFilePath, stringChangeTime);
            //}

            SaveBackup(targetFilePath, stringChangeTime, operationType);
        }

        private void LogMaintain(string targetFilePath, string oldTargetFilePath)
        {
            string stringChangeTime = DateTime.Now.ToString("dd.MM.yyyy-HH.mm.ss");

            LogWrite(targetFilePath, oldTargetFilePath, stringChangeTime);

            SaveBackup(targetFilePath, oldTargetFilePath, stringChangeTime);
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
                    $"FROM: {oldTargetPathstring} TO: {targetPath}");
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

        private void SaveBackup(string targetFilePath, string stringChangeTime, WatcherChangeTypes operationType)
        {
            string fileName = targetFilePath.Remove(0, targetFilePath.LastIndexOf('\\'));

            string[] tempFiles = Directory.GetFiles(TempFolder.FullName);

            for (int i = 0; i < tempFiles.Length; i++)
            {
                string tempFileName = tempFiles[i].Remove(0, tempFiles[i].LastIndexOf('\\'));

                if (tempFileName == fileName)
                {
                    DirectoryInfo logPath = new DirectoryInfo(targetFilePath.Replace(RootDirectory.Name,
                                $"{RootDirectory.Name}\\{LogFolder.Name}\\{stringChangeTime}"));

                    string logFolderPath = logPath.FullName.Remove(logPath.FullName.LastIndexOf('\\'));

                    if (operationType == WatcherChangeTypes.Changed || operationType == WatcherChangeTypes.Deleted)
                    {
                        Directory.CreateDirectory(logFolderPath);
                        File.Copy(TempFolder.FullName + fileName, logPath.FullName);
                        return;
                    }

                    return;

                    //switch (operationType)
                    //{
                        

                    //    case WatcherChangeTypes.Created:
                    //        return;

                    //    case WatcherChangeTypes.Deleted:
                    //        Directory.CreateDirectory(logFolderPath);
                    //        File.Copy(TempFolder.FullName + fileName, logPath.FullName);
                    //        return;

                    //    case WatcherChangeTypes.Changed:
                    //        Directory.CreateDirectory(logFolderPath);
                    //        File.Copy(TempFolder.FullName + fileName, logPath.FullName);
                    //        return;

                    //    case WatcherChangeTypes.Renamed:
                    //        //TODO
                    //        return;

                    //    default:
                    //        throw new Exception();
                    //        return;
                    //}
                }
            }
        }

        private void SaveBackup(string targetFilePath, string oldTargetFilePath, string stringChangeTime)
        {
            string fileName = targetFilePath.Remove(0, targetFilePath.LastIndexOf('\\'));
            string oldFileName = oldTargetFilePath.Remove(0, oldTargetFilePath.LastIndexOf('\\'));

            string[] tempFiles = Directory.GetFiles(TempFolder.FullName);

            for (int i = 0; i < tempFiles.Length; i++)
            {
                string tempFileName = tempFiles[i].Remove(0, tempFiles[i].LastIndexOf('\\'));

                if (tempFileName == oldFileName)
                {
                    DirectoryInfo logPath = new DirectoryInfo(oldTargetFilePath.Replace(RootDirectory.Name,
                                $"{RootDirectory.Name}\\{LogFolder.Name}\\{stringChangeTime}"));

                    string logFolderPath = logPath.FullName.Remove(logPath.FullName.LastIndexOf('\\'));

                    Directory.CreateDirectory(logFolderPath);
                    File.Copy(TempFolder.FullName + oldFileName, logPath.FullName);
                }
            }
        }

        public void ReturnChanges(int targetIndex)
        {
            List<string[]> logData = GetLogData();

            int returnCount = logData.Count - targetIndex;
            int currentStep = 0;

            while (currentStep != returnCount)
            {
                string[] currentLogData = logData[logData.Count - currentStep - 1];

                for (int i = 0; i < currentLogData.Length; i++)
                {
                    currentLogData[i] = currentLogData[i].Trim();
                }

                DataReturn(currentLogData);
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

        private void DataReturn(string[] currentLogData)
        {
            //logData[0] - string data
            //logData[1] - action type
            //logData[2] - path where cahnge was occures

            switch (currentLogData[1])
            {
                case "CHANGED":
                    //string targetPath = currentLogData[2].Replace($"{LogFolder.Name}\\{currentLogData[0]}", "");

                    //if (File.Exists(targetPath))
                    //{
                    //    File.Delete(targetPath);
                    //}

                    //string targetFileDirectory = targetPath.Remove(targetPath.LastIndexOf('\\'));

                    //if (!Directory.Exists(targetFileDirectory))
                    //{
                    //    Directory.CreateDirectory(targetFileDirectory);
                    //}

                    //File.Move(currentLogData[2], targetPath);

                    DataRestore(currentLogData);
                    
                    return;

                case "CREATED":
                    if (File.Exists(currentLogData[2]))
                    {
                        File.Delete(currentLogData[2]);
                    }
                    return;

                case "RENAMED":
                    //TODO: restore old name
                    break;

                case "DELETED":
                    DataRestore(currentLogData);
                    break;

                default:
                    break;
            }
        }

        private void DataRestore(string[] logData)
        {
            string targetPath = logData[2].Replace($"{LogFolder.Name}\\{logData[0]}", "");

            string logPath = logData[2].Replace(RootDirectory.Name,
                $"{RootDirectory.Name}\\{LogFolder.Name}\\{logData[0]}");

            if (File.Exists(targetPath))
            {
                File.Delete(targetPath);
            }

            string targetFileDirectory = targetPath.Remove(targetPath.LastIndexOf('\\'));

            if (!Directory.Exists(targetFileDirectory))
            {
                Directory.CreateDirectory(targetFileDirectory);
            }

            File.Move(logPath, logData[2]);
        }

        private void DataDelete()
        {

        }
    }
}
