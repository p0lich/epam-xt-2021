using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EPAM_Task3._1
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Task 3.1.1
            //WeakestLink();

            // Task 3.1.2
            TextAnalysisMenu();
        }

        public static int InputPositiveInteger()
        {
            do
            {
                if (Int32.TryParse(Console.ReadLine(), out int value))
                {
                    if (value > 0)
                    {
                        return value;
                    }                   
                }

                Console.WriteLine("Wrong input. Try again");
            } while (true);
        }

        #region Task 3.1.1

        public static void WeakestLink()
        {
            InputGameSettings(out int N, out int stepSize);

            List<bool> activeHumans = new List<bool>();

            for (int i = 0; i < N; i++)
            {
                activeHumans.Add(true);
            }

            int currentIndex = 0;

            if (activeHumans.Count < stepSize)
            {
                Console.WriteLine("Size of step cannot be more then humans count");
                return;
            }

            while (activeHumans.Count >= stepSize)
            {
                EliminateHuman(ref activeHumans, ref currentIndex, stepSize);
                Console.WriteLine("One human was eliminated. Remaining humans: " + activeHumans.Count);
            }

            Console.WriteLine("Game over. Cannot eliminate more humans");
        }

        private static void InputGameSettings(out int humanCount, out int stepSize)
        {
            Console.Write("Input humans count: ");
            humanCount = InputPositiveInteger();

            Console.Write("Enter which person will be crossed out each round: ");
            stepSize = InputPositiveInteger();
        }

        private static void EliminateHuman(ref List<bool> activeHumans, ref int currentHuman, int stepSize)
        {
            activeHumans.RemoveAt((currentHuman + stepSize) % activeHumans.Count);
            currentHuman = (currentHuman + stepSize) % activeHumans.Count;
        }

        #endregion

        #region Task 3.1.2

        public static void TextAnalysisMenu()
        {
            string mainMenu =
                "1 - read or write\n" +
                "2 - analyse text\n" +
                "3 - exit\n";

            int option;
            string text = "";

            do
            {
                Console.WriteLine(mainMenu);

                option = InputPositiveInteger();

                switch (option)
                {
                    case 1:
                        text = GetText();
                        break;

                    case 2:
                        if (String.IsNullOrEmpty(text))
                        {
                            Console.WriteLine("Text is empty");
                            continue;
                        }

                        else
                        {
                            ShowResults(GetTextAnalysis(text));
                        }

                        break;

                    case 3:
                        return;

                    default:
                        Console.WriteLine("Wrong input");
                        break;
                }
            } while (true);
        }

        public static void ShowResults(Dictionary<string, int> analysedText)
        {
            Console.WriteLine("3 most used words:");

            for (int i = 1; i <= 3; i++)
            {
                Console.WriteLine("{0}: {1}",
                    analysedText.ElementAt(analysedText.Count - i).Key,
                    analysedText.ElementAt(analysedText.Count - i).Value);
            }

            int minWordCount = analysedText.ElementAt(0).Value;

            Console.WriteLine("\nMost unused words in text:");

            for (int i = 0; i < analysedText.Count; i++)
            {
                if (analysedText.ElementAt(i).Value == minWordCount)
                {
                    Console.Write(analysedText.ElementAt(i).Key + " ");
                }

                else
                {
                    Console.WriteLine("\nWord(s) was used " + minWordCount + " times\n");
                    return;
                }
            }

            //Console.WriteLine("3 most rarest words");

            //for (int i = 0; i < 3; i++)
            //{
            //    Console.WriteLine("{0}: {1}",
            //        analysedText.ElementAt(i).Key,
            //        analysedText.ElementAt(i).Value);
            //}
        }

        private static string GetText()
        {
            string text;

            string inputMenuOption =
                            "1 - input text\n" +
                            "2 - load text\n";

            int inputOption;

            do
            {
                Console.WriteLine(inputMenuOption);
                inputOption = InputPositiveInteger();

                switch (inputOption)
                {
                    case 1:
                        text = Console.ReadLine();
                        return text;

                    case 2:
                        Console.WriteLine("Input path:");
                        string path = Console.ReadLine();
                        text = ReadFile(path);
                        return text;

                    default:
                        Console.WriteLine("Wrong input");
                        break;
                }
            } while (true);
        }

        private static string ReadFile(string path)
        {
            string text = "";

            try
            {
                StreamReader sr = new StreamReader(path);
                text = sr.ReadToEnd();
                sr.Close();
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return text;
        }

        private static char[] GetSeparatorSymbols(string text)
        {
            List<char> punctString = new List<char>() { ' ' };
            for (int i = 0; i < text.Length; i++)
            {
                if (Char.IsPunctuation(text[i]) || Char.IsControl(text[i]))
                {
                    punctString.Add(text[i]);
                }
            }

            return punctString.ToArray();
        }

        private static Dictionary<string, int> GetTextAnalysis(string text)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();

            char[] separatorSybmbols = GetSeparatorSymbols(text);
            string[] separatedText = text.ToLower().Split(separatorSybmbols, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < separatedText.Length; i++)
            {
                if (dict.ContainsKey(separatedText[i]))
                {
                    dict[separatedText[i]]++;
                }

                else
                {
                    dict.Add(separatedText[i], 1);
                }
            }

            return dict.OrderBy(pair => pair.Value).
                ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        #endregion
    }
}
