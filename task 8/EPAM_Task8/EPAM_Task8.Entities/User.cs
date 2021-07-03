using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_Task8.Entities
{
    public class User
    {
        private int _age;
        private string _name;

        public Guid Id { get; set; }
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(string.Format("Name cannot be empty"));
                }

                _name = value;
            }
        }
        public DateTime DateOfBirth { get; set; }

        // need rework
        public int Age {
            get
            {
                return _age;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(string.Format("The age must be larger than 0"));
                }

                _age = value;
            }
        }

        public List<Guid> AwardsId { get; set; }

        public User() { }

        public User(string name, DateTime dateOfBirth, int age)
        {
            Id = Guid.NewGuid();
            Name = name;
            DateOfBirth = dateOfBirth;
            Age = age;
            AwardsId = new List<Guid>();
        }

        public User(string name, DateTime dateOfBirth, int age, List<Guid> awards)
        {
            Id = Guid.NewGuid();
            Name = name;
            DateOfBirth = dateOfBirth;
            Age = age;
            AwardsId = new List<Guid>(awards);
        }

        public User(Guid id, User user)
        {
            Id = id;
            Name = user.Name;
            DateOfBirth = user.DateOfBirth;
            Age = user.Age;
            AwardsId = new List<Guid>(user.AwardsId);
        }

        public void GetAward(Award award)
        {
            AwardsId.Add(award.Id);
        }
    }
}
