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
            DynamicArray<string> emptyMass = new DynamicArray<string>();
            ShowInfo(emptyMass);

            DynamicArray<float> emptyWithLengthMass = new DynamicArray<float>(10);
            ShowInfo(emptyWithLengthMass);

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

            mass.Remove(100);

            Console.WriteLine(mass[100]);
        }

        public static void ShowInfo<T>(DynamicArray<T> mass)
        {
            StringBuilder str = new StringBuilder();

            //for (int i = 0; i < mass.Length; i++)
            //{
            //    str.Append(mass[i] + " ");
            //}

            foreach (var item in mass)
            {
                str.Append(item + " ");
            }

            Console.WriteLine("Data: {0}\nLength: {1}\nCapacity: {2}\n",
                str.ToString(),
                mass.Length,
                mass.Capacity);
        }
    }
}
