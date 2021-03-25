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

            if (!ErrorInput(N, stepSize))
            {
                while (IsEliminateAvaible(activeHumans, stepSize))
                {
                    EliminateHuman(ref activeHumans, ref currentIndex, stepSize);
                    RoundEndNotification(activeHumans.Count);
                }

                EndGameNotification();
            }
        }

        private static void InputGameSettings(out int humanCount, out int stepSize)
        {
            Console.Write("Input humans count: ");
            humanCount = InputPositiveInteger();

            Console.Write("Enter which person will be crossed out each round: ");
            stepSize = InputPositiveInteger();
        }

        private static bool IsEliminateAvaible(List<bool> activeHumans, int stepSize)
        {
            if (activeHumans.Count < stepSize)
            {
                return false;
            }

            return true;
        }

        private static void EliminateHuman(ref List<bool> activeHumans, ref int currentHuman, int stepSize)
        {
            activeHumans.RemoveAt((currentHuman + stepSize) % activeHumans.Count);
            currentHuman = (currentHuman + stepSize) % activeHumans.Count;
        }

        private static void RoundEndNotification(int leftHumansCount)
        {
            Console.WriteLine("One human was eliminated. Remaining humans: " + leftHumansCount);
        }

        private static void EndGameNotification()
        {
            Console.WriteLine("Game over. Cannot eliminate more humans");
        }

        private static bool ErrorInput(int humansCount, int stepSize)
        {
            if (humansCount < stepSize)
            {
                Console.WriteLine("Size of step cannot be more then humans count");
                return true;
            }

            return false;
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
                        string inputMenuOption = 
                            "1 - input text\n" +
                            "2 - load text\n";

                        bool isInput = false;
                        int inputOption;

                        do
                        {
                            Console.WriteLine(inputMenuOption);
                            inputOption = InputPositiveInteger();

                            switch (inputOption)
                            {
                                case 1:
                                    text = Console.ReadLine();
                                    isInput = true;
                                    break;

                                case 2:
                                    Console.WriteLine("Input path:");
                                    string path = Console.ReadLine();
                                    text = ReadFile(path);
                                    isInput = true;
                                    break;

                                default:
                                    Console.WriteLine("Wrong input");
                                    break;
                            }
                        } while (!isInput);

                        break;

                    case 2:
                        if (text == "")
                        {
                            Console.WriteLine("Text is empty");
                        }

                        else
                        {
                            Dictionary<string, int> analysedText = GetTextAnalysis(text);
                            ShowResults(analysedText);
                        }

                        break;

                    case 3:
                        break;

                    default:
                        Console.WriteLine("Wrong input");
                        break;
                }
            } while (option != 3);
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

            Console.WriteLine("3 most rarest words");

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("{0}: {1}",
                    analysedText.ElementAt(i).Key,
                    analysedText.ElementAt(i).Value);
            }
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
            StringBuilder punctString = new StringBuilder(" ");
            for (int i = 0; i < text.Length; i++)
            {
                if (Char.IsPunctuation(text[i]) || Char.IsControl(text[i]))
                {
                    punctString.Append(text[i]);
                }
            }

            return punctString.ToString().ToCharArray();
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
