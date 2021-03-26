using System;
using System.Collections.Generic;
using System.Text;
using ExtendedCollections;

namespace EPAM_Task3._2
{
    class Program
    {
        static void Main(string[] args)
        {
            DynamicArray<int> mass = new DynamicArray<int>(new int[] { 1, 2, 3, 4 });
            ShowInfo(mass);

            mass.Add(10);
            ShowInfo(mass);

            mass.AddRange(new int[] { 11, 12, 13, 14 });
            ShowInfo(mass);

            mass.Remove(2);
            ShowInfo(mass);

            mass.Insert(8, -1);
            ShowInfo(mass);

            mass.Insert(0, -2);
            ShowInfo(mass);

            //List<int> lst = new List<int>();

            //for (int i = 0; i < 4; i++)
            //{
            //    lst.Add(i);
            //}

            //lst.Insert(4, 9);

            //for (int i = 0; i < lst.Count; i++)
            //{
            //    Console.WriteLine(lst[i]);
            //}
        }

        public static void ShowInfo<T>(DynamicArray<T> mass)
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < mass.Length; i++)
            {
                str.Append(mass[i] + " ");
            }

            Console.WriteLine("Data: {0}\nLength: {1}\nCapacity: {2}\n",
                str.ToString(),
                mass.Length,
                mass.Capacity);
        }
    }
}
