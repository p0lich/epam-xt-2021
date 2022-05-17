using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_Task8.Entities
{
    public class Award
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public List<Guid> UsersId { get; set; }

        public Award() { }

        public Award(string title)
        {
            Id = Guid.NewGuid();
            Title = title;
            UsersId = new List<Guid>();
        }

        public Award(string title, List<Guid> users)
        {
            Id = Guid.NewGuid();
            Title = title;
            UsersId = new List<Guid>(users);
        }

        public Award(Guid id, Award award)
        {
            Id = id;
            Title = award.Title;
            UsersId = new List<Guid>(award.UsersId);
        }

        public void AddUser(User user)
        {
            UsersId.Add(user.Id);
        }
    }
}
