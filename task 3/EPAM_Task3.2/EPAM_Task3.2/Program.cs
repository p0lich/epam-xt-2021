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
            try
            {
                DynamicArray<string> emptyMass = new DynamicArray<string>();
                Console.WriteLine("Empty constructor");
                ShowInfo(emptyMass);

                DynamicArray<float> emptyWithLengthMass = new DynamicArray<float>(10);
                Console.WriteLine("Constructor with parameter");
                ShowInfo(emptyWithLengthMass);

                DynamicArray<double> mass = new DynamicArray<double>(new double[] { 1, 2, 3, 4 });
                Console.WriteLine("Constructor with collection");
                ShowInfo(mass);

                mass.Add(10);
                Console.WriteLine("Add");
                ShowInfo(mass);

                mass.AddRange(new double[] { 11, 12, 13, 14 });
                Console.WriteLine("AddRange");
                ShowInfo(mass);

                mass.Remove(2);
                Console.WriteLine("Remove by index");
                ShowInfo(mass);

                mass.Remove(2.0);
                Console.WriteLine("Remove by value");
                ShowInfo(mass);

                mass.Insert(7, -6);
                Console.WriteLine("Insert");
                ShowInfo(mass);

                mass.Insert(0, -2);
                ShowInfo(mass);

                Console.WriteLine("Negative index\n" + mass[-1]);
                Console.WriteLine("-----");

                mass.Capacity -= 5;
                Console.WriteLine("Capacity change");
                ShowInfo(mass);

                //mass.Capacity -= 5;
                //ShowInfo(mass);

                DynamicArray<double> copyMass = (DynamicArray<double>)mass.Clone();
                Console.WriteLine("Mass copy");
                ShowInfo(copyMass);

                double[] regularMass = mass.ToArray();
                Console.WriteLine("ToArray");

                Console.WriteLine(regularMass.GetType());
                for (int i = 0; i < regularMass.Length; i++)
                {
                    Console.Write(regularMass[i] + " ");
                }

                Console.WriteLine("\n-----");

                CycledDynamicArray<int> cycledMass = new CycledDynamicArray<int>(new int[] { 1, 2, 3 });

                //Console.WriteLine(mass[1000]);

                //foreach (var item in cycledMass)
                //{
                //    Console.WriteLine(item);
                //}
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
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

            Console.WriteLine("-----");
        }
    }
}
