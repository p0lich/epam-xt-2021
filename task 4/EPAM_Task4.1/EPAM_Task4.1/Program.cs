using System;
using System.Collections.Generic;
using System.IO;

namespace EPAM_Task4._1
{
    class Program
    {
        static void Main(string[] args)
        {
            FileWatcher watcher = new FileWatcher(@"C:\Users\Alex\Documents\epam-xt-2021\task 4\TestFolder");
            watcher.StartWatching();

            //List<string> filePaths = FileWatcher.GetAllOpenFilePaths(@"C:\Users\Alex\Documents\epam-xt-2021\task 4\TestFolder"); ;

            //foreach (var item in filePaths)
            //{
            //    Console.WriteLine(item);
            //}

            Console.ReadKey();
        }
    }
}