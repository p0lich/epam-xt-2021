using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_Task2._1._2
{
    class User
    {
        private const string _optionList =
            "1 - Add figure\n" +
            "2 - Show figures\n" +
            "3 - Clear canvas\n" +
            "4 - Close canvas\n" +
            "5 - LogOut\n" +
            "6 - Switch user\n";

        private const string _figureList =
            "1 - Line\n" +
            "2 - Circle\n" +
            "3 - Ring\n" +
            "4 - Rectangle\n" +
            "5 - Quadrate\n" +
            "6 - Triangle\n";

        private string _login, _password;
        private bool _isLogin;
        private FiguresCanvas _canvas;

        public string Login
        {
            get
            {
                return _login;
            }
        }

        public bool IsLogin
        {
            get
            {
                return _isLogin;
            }

            set
            {
                _isLogin = value;
            }
        }

        private User(string login, string password, int canvasWidth, int canvasHeight)
        {
            this._login = login;
            this._password = password;
            this._isLogin = false;
            this._canvas = new FiguresCanvas(canvasWidth, canvasHeight);
        }

        public static User CreateUser(UsersList users, string login, string password, int widht, int height)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Login == login)
                {
                    Console.WriteLine("User with same login already exist");
                    return null;
                }
            }

            return new User(login, password, widht, height);
        }

        public void LogIn()
        {
            string password = InputPassword();

            if (IsPasswordRight(password))
            {
                _isLogin = true;
                LogInOutMessage();
            }
        }

        public void LogOut()
        {
            _isLogin = false;
            LogInOutMessage();
        }

        public int UserMenu()
        {
            do
            {
                Console.WriteLine(_login + " canvas:\n");
                ShowMenu();
                int option = Program.InputInt();
                Console.WriteLine();

                switch (option)
                {
                    case 1:
                        Console.WriteLine("Add figure:\n" + _figureList);
                        int figureOption = Program.InputInt();
                        Console.WriteLine();

                        if (figureOption < 1 || figureOption > 6)
                        {
                            Console.WriteLine("Wrong input");
                        }

                        else
                        {
                            _canvas.AddFigure(figureOption);
                        }

                        break;

                    case 2:
                        _canvas.ShowFigures();
                        break;

                    case 3:
                        _canvas.ClearCanvas();
                        break;

                    case 4:
                        Console.WriteLine("Cancvas was closed");
                        return 4;

                    case 5:
                        LogOut();
                        return 5;

                    case 6:
                        LogOut();
                        return 6;

                    default:
                        Console.WriteLine("Wrong input");
                        break;

                }
            } while (true);
        }

        private void ShowMenu()
        {
            if (_isLogin)
            {
                Console.WriteLine(_optionList);
            }
        }

        private string InputPassword()
        {
            Console.Write("Input password: ");
            return Console.ReadLine();
        }

        private bool IsPasswordRight(string password)
        {
            if (_password == password)
            {
                return true;
            }

            return false;
        }

        private void LogInOutMessage()
        {
            if (_isLogin)
            {
                Console.WriteLine("User " + _login + " has log in");
                return;
            }

            Console.WriteLine("User " + _login + " has log out");
        }
    }
}
