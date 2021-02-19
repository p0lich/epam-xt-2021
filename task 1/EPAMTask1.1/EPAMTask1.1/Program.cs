using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAMTask1._1
{
    class Program
    {
        // Method that separate input values and return them as array of integer values
        // Input values must be separated by space key
        public static int[] ReadInputValues(int size)
        {
            while (true)
            {
                int[] ConvertedString = new int[size];

                string[] InputValues = Console.ReadLine().Split(new char[] { ' ' });
                bool IsStringCorrect = true;

                for (int i = 0; i < size; i++)
                {
                    if (int.TryParse(InputValues[i], out int num))
                    {
                        ConvertedString[i] = num;
                    }

                    else
                    {
                        IsStringCorrect = false;
                    }
                }

                if (IsStringCorrect)
                {
                    return ConvertedString;
                }
                
                else
                {
                    Console.WriteLine("Some of input values isn't integer. Try again");
                }
            }
        }

        // Task 1.1.1
        public static void CalculateRectangleArea()
        {
            Console.WriteLine("Task 1.1.1\n");

            Console.WriteLine("Input rectangle size:");

            while (true)
            {
                int[] InputValues = ReadInputValues(2);

                if (InputValues[0] <= 0 || InputValues[1] <= 0)
                {
                    Console.WriteLine("Input values must be higher then 0. Try again");
                }
                else
                {
                    Console.WriteLine("Rectangle Area: " + InputValues[0] * InputValues[1]);
                    Console.WriteLine("Task finished");
                    return;
                }
            }
        }

        // Task 1.1.2
        public static void DrawTriangle()
        {
            Console.WriteLine("Task 1.1.2\n");

            Console.Write("Input number: ");
            int N = ReadInputValues(1)[0];

            for (int i = 1; i < N + 1; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }

            Console.WriteLine("Task finished");
        }

        // Task 1.1.3
        public static void DrawAnotherTriangle()
        {
            Console.WriteLine("Task 1.1.3\n");

            Console.Write("Input number: ");
            int N = ReadInputValues(1)[0];

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N - i; j++)
                {
                    Console.Write(" ");
                }

                for (int j = 0; j < 2 * i + 1; j++)
                {
                    Console.Write("*");
                }

                Console.WriteLine();
            }

            Console.WriteLine("Task finished");
        }

        // Task 1.1.4
        static public void DrawXmasTree()
        {
            Console.WriteLine("Task 1.1.4\n");

            Console.Write("Input number: ");
            int N = ReadInputValues(1)[0];

            for (int i = 0; i < N + 1; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    for (int k = 0; k < N - j; k++)
                    {
                        Console.Write(" ");
                    }

                    for (int k = 0; k < 2 * j + 1; k++)
                    {
                        Console.Write("*");
                    }

                    Console.WriteLine();
                }
            }

            Console.WriteLine("Task finished");
        }

        // Task 1.1.5
        static public void CalculateSumOfNumbers()
        {
            Console.WriteLine("Task 1.1.5\n");

            int sum = 0;

            for (int i = 3; i < 1000; i++)
            {
                if (i % 3 == 0 || i % 5 == 0)
                {
                    sum += i;
                }
            }

            Console.WriteLine("Result: " + sum);
            Console.WriteLine("Task finished");
        }

        // Task 1.1.6
        public static void FontAdjustment()
        {
            Console.WriteLine("Task 1.1.6\n");

            string NoneOption, BoldMark, ItalicMark, UnderlineMark;

            NoneOption = "None";
            BoldMark = "Bold";
            ItalicMark = "Italic";
            UnderlineMark = "Underline";

            Dictionary<int, string> ChosenOptions = new Dictionary<int, string>(3);

            int option = -1;

            while (option != 4)
            {
                string ChosenOptionsString = "";

                if (ChosenOptions.Count > 0)
                {
                    ChosenOptions.Remove(0);

                    foreach (KeyValuePair<int, string> keyValue in ChosenOptions)
                    {
                        ChosenOptionsString += keyValue.Value + " ";
                    }
                }

                else
                {
                    ChosenOptions.Add(0, NoneOption);
                    ChosenOptionsString += NoneOption;
                }

                Console.WriteLine(
                    "Label options: " + ChosenOptionsString.Trim().Replace(" ", ", ") + "\n" +
                    "Input:\n" +
                        "\t1: Bold\n" +
                        "\t2: Italic\n" +
                        "\t3: Underline\n" +
                        "\t4: Exit");

                option = Convert.ToInt32(Console.ReadLine());

                if (option == 1)
                {
                    if (!ChosenOptions.ContainsKey(option))
                    {
                        ChosenOptions.Add(option, BoldMark);
                    }

                    else
                    {
                        ChosenOptions.Remove(option);
                    } 
                }

                if (option == 2)
                {
                    if (!ChosenOptions.ContainsKey(option))
                    {
                        ChosenOptions.Add(option, ItalicMark);
                    }

                    else
                    {
                        ChosenOptions.Remove(option);
                    }
                }

                if (option == 3)
                {
                    if (!ChosenOptions.ContainsKey(option))
                    {
                        ChosenOptions.Add(option, UnderlineMark);
                    }

                    else
                    {
                        ChosenOptions.Remove(option);
                    }
                }

                if (option < 1 || option > 4)
                {
                    Console.WriteLine("Wrong input");
                }
            }

            Console.WriteLine("Task finished");
        }

        // Task 1.1.7
        public static void ArrayProcessing()
        {
            Console.WriteLine("Task 1.1.7\n");

            Console.Write("Input array size: ");
            int N = ReadInputValues(1)[0];

            int[] mass = new int[N];
            Random rand = new Random();

            Console.WriteLine("\nInitial array values:");

            for (int i = 0; i < N; i++)
            {
                mass[i] = rand.Next(1000);
                Console.Write(mass[i] + " ");
            }

            Console.WriteLine();

            // Bubble sort
            for (int i = 0; i < mass.Length; i++)
            {
                for (int j = mass.Length - 1; j > i; j--)
                {
                    if (mass[j - 1] > mass[j])
                    {
                        int temp = mass[j - 1];
                        mass[j - 1] = mass[j];
                        mass[j] = temp;
                    }
                }
            }

            Console.WriteLine(
                "\nMin value: " + mass[0] + "\n" + 
                "Max value: " + mass[N - 1]);

            Console.WriteLine("\nResult:");

            for (int i = 0; i < N; i++)
            {
                Console.Write(mass[i] + " ");
            }

            Console.WriteLine("\nTask finished");
        }

        // Task 1.1.8
        public static void PositiveNullifier()
        {
            Console.WriteLine("Task 1.1.8\n");

            Console.Write("Input array size: ");
            int[] DimensionValues = ReadInputValues(3);

            int N = DimensionValues[0];
            int M = DimensionValues[1];
            int L = DimensionValues[2];

            int[,,] mass = new int[N, M, L];
            Random rand = new Random();

            Console.WriteLine("\nInitial array values:");

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    for (int k = 0; k < L; k++)
                    {
                        mass[i, j, k] = rand.Next(200) - 100;
                        Console.Write(mass[i, j, k] + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }

            Console.WriteLine("\n\nResult:");

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    for (int k = 0; k < L; k++)
                    {
                        if (mass[i, j, k] > 0)
                        {
                            mass[i, j, k] = 0;
                        }
                        Console.Write(mass[i, j, k] + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }

            Console.WriteLine("Task finished");
        }

        // Task 1.1.9
        public static void NonNegativeSumCalculate()
        {
            Console.WriteLine("Task 1.1.9\n");

            Console.Write("Input array size: ");
            int N = ReadInputValues(1)[0];

            int[] mass = new int[N];
            Random rand = new Random();
            int sum = 0;

            Console.WriteLine("\nInitial array values:");

            for (int i = 0; i < N; i++)
            {
                mass[i] = rand.Next(200) - 100;
                if (mass[i] > 0)
                {
                    sum += mass[i];
                }
                Console.Write(mass[i] + " ");
            }

            Console.WriteLine("\n\nReuslt: " + sum);
            Console.WriteLine("Task finished");
        }

        // Task 1.1.10
        public static void TwoDimensionArrayProcessing()
        {
            Console.WriteLine("Task 1.1.10\n");

            Console.Write("Input array size: ");
            int[] DimensionValues = ReadInputValues(2);

            int N = DimensionValues[0];
            int M = DimensionValues[1];

            int[,] mass = new int[N, M];
            Random rand = new Random();
            int sum = 0;

            Console.WriteLine("\nInitial array values:");

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    mass[i, j] = rand.Next(100);
                    Console.Write(mass[i, j] + " ");
                }
                Console.WriteLine();
            }

            for (int i = 0; i < N; i++)
            {
                for (int j = i % 2; j < M; j += 2)
                {
                    sum += mass[i, j];
                }
            }

            Console.WriteLine("\nResult: " + sum);
            Console.WriteLine("Task finished");
        }

        static void Main(string[] args)
        {
            int Options = -1;

            string CommandMenu =
                "Chose task:\n" +
                "1: task 1.1.1\n" +
                "2: task 1.1.2\n" +
                "3: task 1.1.3\n" +
                "4: task 1.1.4\n" +
                "5: task 1.1.5\n" +
                "6: task 1.1.6\n" +
                "7: task 1.1.7\n" +
                "8: task 1.1.8\n" +
                "9: task 1.1.9\n" +
                "10: task 1.1.10\n" +
                "0: show commands\n" +
                "11: exit";

            Console.WriteLine(
                CommandMenu);

            while (Options != 11)
            {
                Options = Convert.ToInt32(Console.ReadLine());

                if (Options == 0) Console.WriteLine(CommandMenu);

                if (Options == 1) CalculateRectangleArea();

                if (Options == 2) DrawTriangle();

                if (Options == 3) DrawAnotherTriangle();

                if (Options == 4) DrawXmasTree();

                if (Options == 5) CalculateSumOfNumbers();

                if (Options == 6) FontAdjustment();

                if (Options == 7) ArrayProcessing();

                if (Options == 8) PositiveNullifier();

                if (Options == 9) NonNegativeSumCalculate();

                if (Options == 10) TwoDimensionArrayProcessing();

                if (Options == 11) break;

                if (Options < 0 || Options > 11)
                {
                    Console.WriteLine("Wrong input");
                }
            }

            Console.WriteLine("Press any key to close the application");
            Console.ReadKey();
        }
    }
}
