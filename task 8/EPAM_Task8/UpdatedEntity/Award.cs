using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdatedEntity
{
    class Award
    {
        public int Id { get; }
        public string Title { get; private set; }

        public Award(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
