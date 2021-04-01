using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_Task3._3._3
{
    public class Pizza
    {
        public string Name { get; }
        public int CookingTime { get; }
        public int Price { get; }

        public Pizza(string name, int preparingTime, int price)
        {
            Name = name;
            CookingTime = preparingTime;
            Price = price;
        }
    }
}
