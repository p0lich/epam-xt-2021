using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM_Task3._3._3
{
    public class Client
    {
        public string Name { get; private init; }
        public int Balance { get; set; }
        public List<Order> Orders { get; set; }
        public List<Order> OrdersHistory { get; set; }

        public Client(string name, int balance)
        {
            Name = name;
            Balance = balance;
            Orders = new List<Order>();
            OrdersHistory = new List<Order>();
        }

        public void ShowOrderHistory()
        {
            foreach (var order in OrdersHistory)
            {
                StringBuilder pizzas = new StringBuilder();

                foreach (var pizza in order.Pizzas)
                {
                    pizzas.Append(pizza.Name + " ");
                }

                Console.WriteLine("{0}: {1}\nPrice: {2}",
                    order.OrderNumber,
                    pizzas.ToString(),
                    order.TotalPrice);
            }
        }
    }
}
