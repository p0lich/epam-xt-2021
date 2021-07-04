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
                    throw new ArgumentOutOfRangeException(string.Format("Wrong input of birth date"));
                }

                _dateOfBirth = value;
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

        public List<Award> Awards { get; set; }

        public User(int id, bool isAdmin, DateTime dateOfBirth, string name, string password)
        {
            Id = id;
            IsAdmin = isAdmin;
            DateOfBirth = dateOfBirth;
            Name = name;
            Password = password;
            Awards = new List<Award>();
        }

        public User(int id, bool isAdmin, DateTime dateOfBirth, string name, string password, List<Award> awards)
        {
            Id = id;
            IsAdmin = isAdmin;
            DateOfBirth = dateOfBirth;
            Name = name;
            Password = password;
            Awards = new List<Award>(awards);
        }

        public string GetPassword()
        {
            return Password;
        }
    }
}
