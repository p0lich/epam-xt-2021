using System;

namespace EPAM_Task2._1._2
{
    class Program
    {
        static void Main(string[] args)
        {
            int option;

            FiguresCanvas canvas = new FiguresCanvas(50, 50);

            string optionList =
                "1 - Add figure\n" +
                "2 - Show figures\n" +
                "3 - Clear canvas\n" +
                "4 - Exit\n";

            string figureList =
                "1 - Line\n" +
                "2 - Circle\n" +
                "3 - Ring\n" +
                "4 - Rectangle\n" +
                "5 - Quadrate\n" +
                "6 - Triangle\n";

            do
            {
                Console.WriteLine("Choose option:\n" + optionList);
                Int32.TryParse(Console.ReadLine(), out option);
                Console.WriteLine();

                switch (option)
                {
                    case 1:
                        Console.WriteLine("Add figure:\n" + figureList);

                        Int32.TryParse(Console.ReadLine(), out int figureOption);
                        Console.WriteLine();

                        if (figureOption < 1 || figureOption > 6)
                        {
                            Console.WriteLine("Wrong input");
                        }

                        else
                        {
                            canvas.AddFigure(figureOption);
                        }

                        break;

                    case 2:
                        canvas.ShowFigures();
                        break;

                    case 3:
                        canvas.ClearCanvas();
                        break;

                    case 4:
                        break;

                    default:
                        Console.WriteLine("Wrong input");
                        break;

                }
            } while (option != 4);
        }
    }
}
