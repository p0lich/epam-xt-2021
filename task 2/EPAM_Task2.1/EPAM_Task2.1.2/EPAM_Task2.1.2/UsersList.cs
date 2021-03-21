using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_Task2._1._2
{
    class UsersList
    {
        private List<User> _users;

        public int Count
        {
            get
            {
                return _users.Count;
            }
        }

        public UsersList()
        {
            _users = new List<User>();
        }

        public User this[int index]
        {
            get
            {
                return _users[index];
            }
        }

        public void AddUser(string login, string password, int width, int height)
        {
            User user = User.CreateUser(this, login, password, width, height);

            if (user != null)
            {
                _users.Add(user);
            }

        }

        public void AddUser(User user)
        {
            if (!IsExist(user.Login))
            {
                _users.Add(user);
                return;
            }
        }

        public int SwitchUser()
        {
            Console.WriteLine("Input new login");
            string newLogin = Console.ReadLine();

            if (IsExist(newLogin))
            {
                return SwitchUser(newLogin);
            }

            Console.WriteLine("There is no user with such name");
            return -1;
        }

        public int SwitchUser(string newLogin)
        {
            for (int i = 0; i < _users.Count; i++)
            {
                if (_users[i].Login == newLogin)
                {
                    _users[i].LogIn();
                    return i;
                }
            }

            return -1;
        }

        public void UserSession(int userIndex)
        {
            if (_users[userIndex].IsLogin)
            {
                int userInput = _users[userIndex].UserMenu();

                if (userInput == 6)
                {
                    int newUserIndex = SwitchUser();

                    if (newUserIndex > 0)
                    {
                        UserSession(newUserIndex);
                    }
                }
            }
        }

        private bool IsExist(string login)
        {
            for (int i = 0; i < _users.Count; i++)
            {
                if (_users[i].Login == login)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
