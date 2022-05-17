using System;
using System.Collections.Generic;
using System.IO;

namespace EPAM_Task4._1
{
    class Program
    {
        static void Main(string[] args)
        {
            string menu =
                "1 - watch for changes\n" +
                "2 - restore data\n" +
                "3 - exit";

            int option;

            Console.WriteLine("Input path to working folder:");
            //string rootPath = @"C:\Users\Alex\Documents\epam-xt-2021\task 4\TestFolder";
            string rootPath = Console.ReadLine();
            
            FileWatcher watcher = new FileWatcher(rootPath);

            do
            {
                Console.WriteLine(menu);

                option = InputInteger();

                switch (option)
                {
                    case 1:
                        watcher.StartWatching();
                        Console.WriteLine("Maintain have been started");
                        break;

                    case 2:
                        watcher.EndWatching();
                        int targetDate = ShowAndInputDataIndex(watcher);
                        watcher.ReturnChanges(targetDate);
                        break;

                    case 3:
                        return;

                    default:
                        Console.WriteLine("Wrong input");
                        break;
                }
            } while (true);
        }

        public static int InputInteger()
        {
            while (true)
            {
                if (Int32.TryParse(Console.ReadLine(), out int num))
                {
                    return num;
                }
            }
        }

        public static int ShowAndInputDataIndex(FileWatcher watcher)
        {
            List<string> changeDates = watcher.GetChangeDates();

            for (int i = 0; i < changeDates.Count; i++)
            {
                Console.WriteLine((i + 1).ToString() + " - " + changeDates[i]);
            }

            int targetDate;

            do
            {
                targetDate = InputInteger();

                if (targetDate <= changeDates.Count)
                {
                    return targetDate;
                }

                Console.WriteLine("Wrong input. Try again");
            } while (true);
        }
    }
}