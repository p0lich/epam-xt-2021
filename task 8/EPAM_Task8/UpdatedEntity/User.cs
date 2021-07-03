using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdatedEntity
{
    public class User
    {
        private DateTime _dateOfBirth;

        public int Id { get; }
        public string Name { get; private set; }
        public bool IsAdmin { get; private set; }

        public DateTime DateOfBirth
        {
            get
            {
                return _dateOfBirth;
            }

            private set
            {
                if (DateTime.Now.CompareTo(value) <= 0)
                {
                    throw new ArgumentOutOfRangeException(string.Format("Wrong input of date of birth"));
                }
            }
        }

        public int Age
        {
            get
            {
                return DateTime.Now.Year - DateOfBirth.Year;
            }
        }

        private string Password { get; set; }

        public User(bool isAdmin, DateTime dateOfBirth, string name, string password)
        {
            IsAdmin = isAdmin;
            DateOfBirth = dateOfBirth;
            Name = name;
            Password = password;
        }
    }
}
