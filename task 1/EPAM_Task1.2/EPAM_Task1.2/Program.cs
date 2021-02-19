using System;
using System.Linq;
using System.Text;

namespace EPAM_Task1._2
{
    class Program
    {

        public static char[] PunctuationSymbols = new char[] { ' ', '.', ',', '!', '?', '<', '>', '/', '[', ']', '{', '}', '#', '%', '^', '*', '(', ')', ':' };

        // Prepared messages for each task
        public static string PreparedMessageTask1 = "Викентий хорошо отметил день рождения: покушал пиццу, посмотрел кино, пообщался со студентами в чате";
        public static string PreparedFirstMessageTask2 = "написать программу, которая";
        public static string PreparedSecondMessageTask2 = "описание";
        public static string PreparedMessageTask3 = "Антон хорошо начал утро: послушал Стинга, выпил кофе и посмотрел Звёздные Войны";
        public static string PreparedMessageTask4 = "я плохо учил русский язык. забываю начинать предложения с заглавной. хорошо, что можно написать программу!";

        // Task 1.2.1
        public static void CalculateAverageLength(bool IsPrepared)
        {
            Console.WriteLine("Task 1.2.1\n");

            string Message;

            if (IsPrepared)
            {
                Message = PreparedMessageTask1;
                Console.WriteLine("Used prepared message:\n" + Message);
            }

            else
            {
                Console.WriteLine("Input message:");
                Message = Console.ReadLine();
            }

            string[] SplittedMessage = Message.Split(PunctuationSymbols, StringSplitOptions.RemoveEmptyEntries);

            double MedianSum = 0;
            int WordsCount = 0;

            for (int i = 0; i < SplittedMessage.Length; i++)
            {
                MedianSum += SplittedMessage[i].Length;
                WordsCount++;
            }

            // Result without rounding
            MedianSum /= (double)WordsCount;

            Console.WriteLine("Result: " + MedianSum);

            Console.WriteLine("Task finished");
        }

        // Task 1.2.2
        public static void CharDoubler(bool IsPrepared)
        {
            Console.WriteLine("Task 1.2.2\n");

            StringBuilder FirstString;
            string SecondString;

            if (IsPrepared)
            {
                FirstString = new StringBuilder(PreparedFirstMessageTask2);
                SecondString = PreparedSecondMessageTask2;

                Console.WriteLine("Used prepared messages:\nFirst: " + FirstString + "\nSecond: " + SecondString);
            }

            else
            {
                Console.WriteLine("Input message:\nFirst: ");
                FirstString = new StringBuilder(Console.ReadLine());

                Console.WriteLine("Second: ");
                SecondString = Console.ReadLine();
            }

            string ShortenedSecondString = new string(SecondString.Distinct().ToArray());

            for (int i = 0; i < ShortenedSecondString.Length; i++)
            {
                FirstString.Replace(ShortenedSecondString[i].ToString(),
                    ShortenedSecondString[i].ToString() + ShortenedSecondString[i].ToString());
            }

            Console.WriteLine("Result:\n" + FirstString);

            Console.WriteLine("Task finished");
        }

        // Task 1.2.3
        public static void LowercaseCounter(bool IsPrepared)
        {
            Console.WriteLine("Task 1.2.3\n");

            string Message;

            if (IsPrepared)
            {
                Message = PreparedMessageTask3;
                Console.WriteLine("Used prepared message:\n" + Message);
            }

            else
            {
                Console.WriteLine("Input message:");
                Message = Console.ReadLine();
            }

            string[] SplittedMessage = Message.Split(PunctuationSymbols, StringSplitOptions.RemoveEmptyEntries);

            int WordsCount = 0;

            for (int i = 0; i < SplittedMessage.Length; i++)
            {
                if (char.IsLower(SplittedMessage[i][0]))
                {
                    WordsCount++;
                }
            }

            Console.WriteLine("Result: " + WordsCount);

            Console.WriteLine("Task finished");
        }

        // Task 1.2.4
        public static void Validator(bool IsPrepared)
        {
            Console.WriteLine("Task 1.2.4\n");

            char[] SpecSymbols = new char[] { '.', '|', '?', '|', '!' };

            string Message;

            if (IsPrepared)
            {
                Message = PreparedMessageTask4;
                Console.WriteLine("Used prepared message:\n" + Message);
            }

            else
            {
                Console.WriteLine("Input message:");
                Message = Console.ReadLine();
            }

            StringBuilder NewMessage = new StringBuilder(Message);
            NewMessage.Replace(Message[0], char.ToUpper(Message[0]), 0, 1);

            for (int i = 2; i < Message.Length; i++)
            {
                for (int j = 0; j < SpecSymbols.Length; j++)
                {
                    if (Message[i - 2] == SpecSymbols[j])
                    {
                        NewMessage.Replace(Message[i], char.ToUpper(Message[i]), i, 1);
                    }
                }
            }

            Console.WriteLine("Result:\n" + NewMessage);

            Console.WriteLine("Task finished");
        }

        static void Main(string[] args)
        {
            int option = -1;

            string CommandMenu = 
                "Chose task:\n" +
                "1: task 1.2.1\n" +
                "2: task 1.2.2\n" +
                "3: task 1.2.3\n" +
                "4: task 1.2.4\n\n" +
                "Special commands:\n" +
                "0: show commands\n" +
                "5: use prepared inputs in tasks\n   (not enabled by default)\n" +
                "6: show prepared messages\n" +
                "7: exit";

            Console.WriteLine(CommandMenu);

            bool IsPrepared = false;

            while (option != 7)
            {
                option = Convert.ToInt32(Console.ReadLine());

                if (option == 0) Console.WriteLine(CommandMenu);

                if (option == 1) CalculateAverageLength(IsPrepared);

                if (option == 2) CharDoubler(IsPrepared);

                if (option == 3) LowercaseCounter(IsPrepared);

                if (option == 4) Validator(IsPrepared);

                if (option == 5)
                {
                    IsPrepared = !IsPrepared;

                    if (IsPrepared)
                    {
                        Console.WriteLine("You now use prepared messages");
                    }

                    else
                    {
                        Console.WriteLine("Prepared messages was disabled");
                    }
                }

                if (option == 6)
                {
                    Console.WriteLine(
                        "List of prepared messages:\n" +
                        "Task 1.2.1 message:\n" +
                        PreparedMessageTask1 + "\n\n" +

                        "Task 1.2.2 first message:\n" +
                        PreparedFirstMessageTask2 + "\n\n" +

                        "Task 1.2.2 second message:\n" +
                        PreparedSecondMessageTask2 +"\n\n" +

                        "Task 1.2.3 message:\n" +
                        PreparedMessageTask3 +"\n\n" +

                        "Task 1.2.4 message:\n" +
                        PreparedMessageTask4);
                }

                if (option < 0 || option > 7)
                {
                    Console.WriteLine("Wrong input");
                }
            }
        }
    }
}
