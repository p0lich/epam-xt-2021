using System;

namespace EPAM_Task3._3
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] mass = { 1, 2, 3, 5, 1, 3, 1, 9, 1, 1};

            Console.WriteLine("Multiply by 2");
            mass.UpdateData(x => x * 2);

            for (int i = 0; i < mass.Length; i++)
            {
                Console.WriteLine(mass[i]);
            }

            Console.WriteLine("------");

            Console.WriteLine("GetSum: {0}", mass.GetSum());
            Console.WriteLine("GetAverage: {0}", mass.GetAverage());
            Console.WriteLine("GetMostCommon: {0}", mass.GetMostCommon());

            Console.WriteLine("------");

            string rusStr = "тыДЫЩ";
            string enlgStr = "BooM";
            string mixedStr = "Бамs";

            Console.WriteLine("{0}: {1}\n{2}: {3}\n{4}: {5}",
                rusStr,
                rusStr.GetLanguage(),
                enlgStr,
                enlgStr.GetLanguage(),
                mixedStr,
                mixedStr.GetLanguage());
        }
    }
}
