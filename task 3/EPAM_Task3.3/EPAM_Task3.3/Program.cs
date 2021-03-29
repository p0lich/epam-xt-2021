using System;

namespace EPAM_Task3._3
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] mass = { 1, 2, 3, 5, 1, 3, 1, 9, 1, 1};

            //Console.WriteLine(mass.GetSum());
            //Console.WriteLine(mass.GetAverage());
            //Console.WriteLine(mass.GetMostCommon());

            string rusStr = "тыДЫЩ";
            string enlgStr = "BooM";
            string mixedStr = "Бамs";

            Console.WriteLine("{0}\n{1}\n{2}",
                rusStr.GetLanguage(),
                enlgStr.GetLanguage(),
                mixedStr.GetLanguage());
        }
    }
}
