using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EPAM_Task3._3._3
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Pizza> menu = new List<Pizza>
            {
                new Pizza("Pepperoni", 10, 300),
                new Pizza("Carbonara", 15, 400),
                new Pizza("Margarita", 20, 450),
                new Pizza("Ham and mushroms", 12, 400),
                //Test parameters
                new Pizza("Ultrafast pizza", 1, 0),
                new Pizza("Ultraslow pizza", 60, 0),
            };

            Pizzeria pizzeria = new Pizzeria("Pipirouni", menu, 120);

            // Отдельный поток под работу пиццерии.
            // Заказы будут выполнятся до тех пор, пока не закончится время
            //Task.Factory.StartNew(() => pizzeria.WorkingProcess(ReadyOrderNotify));
            pizzeria.StartWorking();
            Thread.Sleep(1000);

            Client client1 = new Client("Bob", 2000);

            client1.SendOrder(pizzeria);

            Thread.Sleep(TimeSpan.FromSeconds(3));
            client1.SendOrder(pizzeria);

            Client client2 = new Client("Tom", 3000);
            client2.SendOrder(pizzeria);
            client2.SendOrder(pizzeria);

            Thread.Sleep(TimeSpan.FromSeconds(60));

            pizzeria.GiveAway(client1);
            //client1.ShowOrdersHistory();

            pizzeria.GiveAway(client1);
            //client.ShowOrdersHistory();

            Console.ReadLine();
        }
    }
}
