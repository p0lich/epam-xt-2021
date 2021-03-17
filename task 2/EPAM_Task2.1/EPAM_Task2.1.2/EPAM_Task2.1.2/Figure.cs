using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_Task2._1._2
{
    public enum FigureType
    {
        None,
        Line,
        Circle,
        Ring,
        Rectangle,
        Quadrate,
        Triangle
    }

    public struct Point
    {
        private double _x;
        private double _y;

        public double X
        {
            get
            {
                return _x;
            }

            set
            {
                _x = value;
            }
        }

        public double Y
        {
            get
            {
                return _y;
            }

            set
            {
                _y = value;
            }
        }

        public Point(double x, double y)
        {
            this._x = x;
            this._y = y;
        }
    }

    abstract public class Figure
    {
        private string _name;
        private Point _center;
        private FigureType _type;

        public string Name
        {
            get
            {
                return _name;
            }

            protected set
            {
                _name = value;
            }
        }

        public Point Center
        {
            get
            {
                return _center;
            }

            set
            {
                _center = value;
            }
        }

        public FigureType Type
        {
            get
            {
                return _type;
            }

            protected set
            {
                _type = value;
            }
        }

        abstract public double Square();
        abstract public double Perimeter();

        abstract public Point[] GetPoints();
    }

    #region Line

    public class Line : Figure
    {
        private Point _endPoint;

        public Point EndPoint
        {
            get
            {
                return _endPoint;
            }

            set
            {
                _endPoint = value;
            }
        }

        public Line(Point startPoint, Point endPoint)
        {
            this.Name = "Line";
            this.Type = FigureType.Line;
            this.Center = startPoint;
            this._endPoint = endPoint;
        }

        public override Point[] GetPoints()
        {
            Point[] points = new Point[2];

            points[0] = Center;
            points[1] = _endPoint;

            return points;
        }

        public override double Perimeter()
        {
            return -1;
        }

        public override double Square()
        {
            return -1;
        }
    }

    #endregion

    #region RoundFigures

    public class RoundFigure : Figure
    {
        private double _radius;

        public double Radius
        {
            get
            {
                return _radius;
            }

            set
            {
                if (value > 0)
                    _radius = value;
            }
        }

        public RoundFigure(Point center, double radius)
        {
            this.Center = center;
            this.Radius = radius;
        }

        public override double Square()
        {
            return Math.PI * Math.Pow(_radius, 2);
        }

        public override double Perimeter()
        {
            return 2 * Math.PI * _radius;
        }

        // Return points in vertical and horizontal directions
        public override Point[] GetPoints()
        {
            Point[] points = new Point[4];

            points[0] = new Point(Center.X - _radius, Center.Y);
            points[1] = new Point(Center.X, Center.Y + _radius);
            points[2] = new Point(Center.X + _radius, Center.Y);
            points[3] = new Point(Center.X, Center.Y - _radius);

            return points;
        }
    }

    public class Circle : RoundFigure
    {
        public Circle(Point center, double radius) : base(center, radius)
        {
            this.Name = "Circle";
            this.Type = FigureType.Circle;
        }
    }

    public class Ring : RoundFigure
    {
        private double _radius2;

        public double Radius2
        {
            get
            {
                return _radius2;
            }

            set
            {
                if (value != Radius)
                {
                    _radius2 = value;
                }
            }
        }

        public Ring(Point center, double radius1, double radius2) : base(center, radius1)
        {
            this.Name = "Ring";
            this.Type = FigureType.Ring;
            this.Radius2 = radius2;
        }

        public override double Perimeter()
        {
            double SecondPerimeter = 2 * Math.PI * _radius2;

            return SecondPerimeter + base.Perimeter();
        }

        public override double Square()
        {
            double SecondSquare = Math.PI * Math.Pow(_radius2, 2);

            return Math.Abs(SecondSquare - base.Square());
        }

        public override Point[] GetPoints()
        {
            Point[] points = new Point[4];

            double r = (Radius > _radius2) ? Radius : _radius2;

            points[0] = new Point(Center.X - r, Center.Y);
            points[1] = new Point(Center.X, Center.Y + r);
            points[2] = new Point(Center.X + r, Center.Y);
            points[3] = new Point(Center.X, Center.Y - r);

            return points;
        }
    }

    #endregion

    #region FourCornerFigures

    public class FourCornerFigure : Figure
    {
        private double _a, _b;

        public double A
        {
            get
            {
                return _a;
            }

            set
            {
                if (value > 0)
                {
                    _a = value;
                }
            }
        }

        public double B
        {
            get
            {
                return _b;
            }

            set
            {
                if (value > 0)
                {
                    _b = value;
                }
            }
        }

        protected FourCornerFigure(Point center, double a, double b)
        {
            this.Center = center;
            this.A = a;
            this.B = b;
        }

        public override double Square()
        {
            return _a * _b;
        }

        public override double Perimeter()
        {
            return 2 * (_a + _b);
        }

        public override Point[] GetPoints()
        {
            Point[] points = new Point[4];

            points[0] = new Point(Center.X - _a / 2, Center.Y - _b / 2);
            points[1] = new Point(Center.X - _a / 2, Center.Y + _b / 2);
            points[2] = new Point(Center.X + _a / 2, Center.Y + _b / 2);
            points[3] = new Point(Center.X + _a / 2, Center.Y - _b / 2);

            return points;
        }
    }

    public class Rectangle : FourCornerFigure
    {
        public Rectangle(Point center, double a, double b) : base(center, a, b)
        {
            this.Name = "Rectangle";
            this.Type = FigureType.Rectangle;
        }
    }

    public class Quadrate : FourCornerFigure
    {
        public Quadrate(Point center, double a) : base(center, a, a)
        {
            this.Name = "Quadrate";
            this.Type = FigureType.Quadrate;
        }
    }

    #endregion

    #region Triangles

    // This is a equilateral triangle
    public class Triangle : Figure
    {
        private double _side;

        public double Side
        {
            get
            {
                return _side;
            }

            set
            {
                if (value > 0)
                {
                    _side = value;
                }
            }
        }

        public Triangle(Point center, double side)
        {
            this.Name = "Triangle";
            this.Type = FigureType.Triangle;
            this.Center = center;
            this.Side = side;
        }

        public override double Perimeter()
        {
            return 3 * _side;
        }

        public override double Square()
        {
            return Math.Pow(_side, 2) * Math.Sqrt(3) / 4;
        }

        public override Point[] GetPoints()
        {
            Point[] points = new Point[3];

            double R = _side * Math.Sqrt(3) / 3;
            double r = _side * Math.Sqrt(3) / 6;

            points[0] = new Point(Center.X - _side / 2, Center.Y - r);
            points[1] = new Point(Center.X, Center.Y + R);
            points[2] = new Point(Center.X + _side / 2, Center.Y - r);

            return points;
        }
    }

    #endregion
}
