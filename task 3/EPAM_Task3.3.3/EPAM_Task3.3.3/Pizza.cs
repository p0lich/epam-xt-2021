using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_Task3._3._3
{
    public class Pizza
    {
        public string Name { get; init; }
        public int CookingTime { get; init; }
        public int Price { get; init;}

        public Pizza(string name, int preparingTime, int price)
        {
            Name = name;
            CookingTime = preparingTime;
            Price = price;
        }
    }
}
