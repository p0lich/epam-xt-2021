using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_Task2._1._2
{
    public class FiguresCanvas
    {
        private readonly int _width, _height;
        private Point center;
        private List<Figure> _figures;

        public int Width
        {
            get
            {
                return _width;
            }

            private init
            {
                if (value > 0)
                {
                    _width = value;
                }
            }
        }

        public int Height
        {
            get
            {
                return _height;
            }

            private init
            {
                if (value > 0)
                {
                    _height = value;
                }
            }
        }

        public FiguresCanvas(int width, int heigth)
        {
            this.Width = width;
            this.Height = heigth;
            this._figures = new List<Figure>();
            this.center = new Point(width / 2, heigth / 2); // the center of canvas is start of coordinates
        }

        public void AddFigure(Figure figure)
        {
            if (CheckPosition(figure))
                _figures.Add(figure);
        }

        public void AddFigure(int figureInd)
        {
            AddFigure(CreateFigure(figureInd));
        }

        public void ShowFigures()
        {
            if (_figures.Count == 0)
            {
                Console.WriteLine("Canvas is clear\n");
            }

            foreach (var item in _figures)
            {
                FigureType type = item.Type;
                double side1, side2;

                if (type == FigureType.Rectangle || type == FigureType.Ring)
                {
                    RestoreData(item, out side1, out side2);
                    Showinfo(item, side1, side2);
                }

                else
                {
                    RestoreData(item, out side1);
                    Showinfo(item, side1);
                }
            }
        }

        public void ClearCanvas()
        {
            _figures.Clear();
        }

        private Point InputPoits(string pointDescription)
        {
            Console.Write(pointDescription + " point X: ");
            Double.TryParse(Console.ReadLine(), out double x);

            Console.Write(pointDescription + " point Y: ");
            Double.TryParse(Console.ReadLine(), out double y);

            return new Point(x, y);
        }

        private void InputFigureProperties(out Point startPoint, out Point endPoint)
        {
            startPoint = InputPoits("Start");
            endPoint = InputPoits("End");

            Console.WriteLine();
        }

        private void InputFigureProperties(FigureType type, out Point center, out double side)
        {
            center = InputPoits("Center");

            if (type == FigureType.Circle)
            {
                Console.Write("Radius: ");
            }

            else
            {
                Console.Write("Side: ");
            }

            Double.TryParse(Console.ReadLine(), out double s);
            side = s;

            Console.WriteLine();
        }

        private void InputFigureProperties(FigureType type, out Point center, out double side1, out double side2)
        {
            center = InputPoits("Center");

            if (type == FigureType.Ring)
            {
                Console.Write("Radius1: ");
                Double.TryParse(Console.ReadLine(), out double s1);
                side1 = s1;

                Console.Write("Radius2: ");
                Double.TryParse(Console.ReadLine(), out double s2);
                side2 = s2;
            }

            else
            {
                Console.Write("Side1: ");
                Double.TryParse(Console.ReadLine(), out double s1);
                side1 = s1;

                Console.Write("Side2: ");
                Double.TryParse(Console.ReadLine(), out double s2);
                side2 = s2;
            }

            Console.WriteLine();
        }

        private Figure CreateFigure(int figureInd)
        {
            Point center, endPoint;
            double side1, side2;

            FigureType type = (FigureType)figureInd;

            switch (type)
            {
                case FigureType.Line:
                    InputFigureProperties(out center, out endPoint);
                    return new Line(center, endPoint);

                case FigureType.Circle:
                    InputFigureProperties(type, out center, out side1);
                    return new Circle(center, side1);

                case FigureType.Ring:
                    InputFigureProperties(type, out center, out side1, out side2);
                    return new Ring(center, side1, side2);

                case FigureType.Rectangle:
                    InputFigureProperties(type, out center, out side1, out side2);
                    return new Rectangle(center, side1, side2);

                case FigureType.Quadrate:
                    InputFigureProperties(type, out center, out side1);
                    return new Quadrate(center, side1);

                case FigureType.Triangle:
                    InputFigureProperties(type, out center, out side1);
                    return new Triangle(center, side1);

                default:
                    return null;
            }
        }

        private bool CheckPosition(Figure figure)
        {
            Point[] points = figure.GetPoints();

            if ((Math.Abs(figure.Center.X) > center.X) || (Math.Abs(figure.Center.Y) > center.Y))
            {
                return false;
            }

            for (int i = 0; i < points.Length; i++)
            {
                if ((Math.Abs(points[i].X) > center.X) || (Math.Abs(points[i].Y) > center.Y))
                {
                    return false;
                }
            }

            return true;
        }

        private void RestoreData(Figure figure, out double side)
        {
            Point[] points = figure.GetPoints();

            switch (figure.Type)
            {
                case FigureType.Line:
                    side = Math.Sqrt(Math.Pow(points[1].X - points[0].X, 2) + Math.Pow(points[1].Y - points[0].Y, 2));
                    return;
                case FigureType.Circle:
                    side = figure.Center.X - points[0].X;
                    return;

                case FigureType.Quadrate:
                    side = points[1].Y - points[0].Y;
                    return;

                case FigureType.Triangle:
                    side = points[2].X - points[0].X;
                    return;

                default:
                    side = -1;
                    return;
            }
        }

        private void RestoreData(Figure figure, out double side1, out double side2)
        {
            Point[] points = figure.GetPoints();

            switch (figure.Type)
            {
                case FigureType.Ring:
                    side1 = figure.Center.X - points[0].X;
                    side2 = Math.Sqrt(side1 * side1 - figure.Square() / Math.PI);
                    return;

                case FigureType.Rectangle:
                    side1 = points[1].Y - points[0].Y;
                    side2 = points[3].X - points[0].X;
                    return;

                default:
                    side1 = -1;
                    side2 = -1;
                    return;
            }
        }

        private void Showinfo(Figure figure, double side)
        {
            Point[] points = figure.GetPoints();
            string strPoints = "";

            for (int i = 0; i < points.Length; i++)
            {
                strPoints += String.Format("({0}; {1}) ", points[i].X, points[i].Y);
            }

            Console.WriteLine(
                "Figure: {0}\n" +
                "Center: ({1}; {2})\n" +
                "Points: {3}",
                figure.Name,
                figure.Center.X,
                figure.Center.Y,
                strPoints
                );

            switch (figure.Type)
            {
                case FigureType.Circle:
                    Console.WriteLine("Radius: " + side);
                    break;

                default:
                    Console.WriteLine("Side: " + side);
                    break;
            }

            if (figure.Type != FigureType.Line)
            {
                Console.WriteLine(
                    "Perimeter: {0}\n" +
                    "Square: {1}\n",
                    figure.Perimeter(), figure.Square());
            }
            else
            {
                Console.WriteLine();
            }
        }

        private void Showinfo(Figure figure, double side1, double side2)
        {
            Point[] points = figure.GetPoints();
            string strPoints = "";

            for (int i = 0; i < points.Length; i++)
            {
                strPoints += String.Format("({0}; {1}) ", points[i].X, points[i].Y);
            }

            Console.WriteLine(
                "Figure: {0}\n" +
                "Center: ({1}; {2})\n" +
                "Points: {3}",
                figure.Name,
                figure.Center.X,
                figure.Center.Y,
                strPoints
                );

            switch (figure.Type)
            {
                case FigureType.Ring:
                    Console.WriteLine("Radius1: " + side1);
                    Console.WriteLine("Radius2: " + side2);
                    break;

                default:
                    Console.WriteLine("Side1: " + side1);
                    Console.WriteLine("Side2: " + side2);
                    break;
            }

            Console.WriteLine(
                "Perimeter: {0}\n" +
                "Square: {1}\n",
                figure.Perimeter(), figure.Square());
        }
    }
}
