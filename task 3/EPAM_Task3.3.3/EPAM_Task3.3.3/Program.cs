using System;
using System.Collections.Generic;

namespace EPAM_Task3._3._3
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Pizza> menu = new List<Pizza>
            {
                new Pizza("Pepperoni", 30, 300),
                new Pizza("Carbonara", 45, 400),
                new Pizza("Margarita", 60, 450),
                new Pizza("Ham and mushroms", 40, 400)
            };

            Pizzeria pizzeria = new Pizzeria("Pipirouni", menu);
            Client client = new Client("Bob", 2000);

            pizzeria.MakeOrder(client);
            pizzeria.MakeOrder(client);

            pizzeria.WorkingProcess(100);

            pizzeria.GiveAway(client);
            client.ShowOrdersHistory();

            Console.WriteLine("------");

            pizzeria.WorkingProcess(100);

            pizzeria.GiveAway(client);
            client.ShowOrdersHistory();
        }
    }
}
