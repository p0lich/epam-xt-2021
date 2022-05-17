using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdatedEntity
{
    public class Award
    {
        public int Id { get; }
        public string Title { get; private set; }
        public List<User> Users { get; set; }

        public Award(int id, string title)
        {
            Id = id;
            Title = title;
            Users = new List<User>();
        }

        public Award(int id, string title, List<User> users)
        {
            Id = id;
            Title = title;
            Users = new List<User>(users);
        }
    }
}
