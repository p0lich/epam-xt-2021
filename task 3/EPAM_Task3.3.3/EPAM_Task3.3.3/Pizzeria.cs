﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EPAM_Task3._3._3
{
    public class Pizzeria
    {
        public List<Pizza> Menu { get; private set; }
        public int LastOrder { get; private set; }
        public List<Order> ReadyToTakeOrders { get; private set; }

        public Pizzeria(List<Pizza> menu)
        {
            Menu = menu;
            LastOrder = 0;
            ReadyToTakeOrders = new List<Order>();
        }

        // 0 - all ok
        // -1 - order is empty
        // -2 - not enough funds

        public int MakeOrder(Client client, ref int clientBalance)
        {
            List<Pizza> pizzasForOrder = ChoosePizzas();

            if (pizzasForOrder.Count == 0)
            {
                return -1;
            }

            int totalPrice = GetPrice(pizzasForOrder);

            if (totalPrice >= clientBalance)
            {
                return -2;
            }

            LastOrder++;
            Order orderForPizzeria = new Order(LastOrder, pizzasForOrder, totalPrice);
            Order orderForClient = new Order(LastOrder, pizzasForOrder, totalPrice);

            clientBalance -= totalPrice;
            client.Orders.Add(orderForClient);

            // Now each cooking processing async, in different threads
            Task.Factory.StartNew(() => OrderCooking(orderForPizzeria));

            return 0;
        }

        private void OrderCooking(Order order)
        {
            Thread.Sleep(TimeSpan.FromSeconds(order.PreparingTime));
            ReadyToTakeOrders.Add(order);
            Console.WriteLine("Order number " + order.OrderNumber + " is ready");
        }

        public bool GiveAway(Client client)
        {
            var clientReadyOrders = from readyOrder in ReadyToTakeOrders
                                    from clientOrder in client.Orders
                                    where readyOrder.OrderNumber == clientOrder.OrderNumber
                                    select clientOrder;

            if (!clientReadyOrders.Any())
            {
                return false;
            }

            foreach (var item in clientReadyOrders.ToList())
            {
                client.OrdersHistory.Add(item);

                int orderInd = ReadyToTakeOrders.FindIndex(o => o.OrderNumber == item.OrderNumber);
                int clientOrderInd = client.Orders.FindIndex(o => o.OrderNumber == item.OrderNumber);

                ReadyToTakeOrders.RemoveAt(orderInd);
                client.Orders.RemoveAt(clientOrderInd);
            }

            return true;
        }

        private int GetPrice(List<Pizza> pizzas)
        {
            int totalPrice = 0;

            for (int i = 0; i < pizzas.Count; i++)
            {
                totalPrice += pizzas[i].Price;
            }

            return totalPrice;
        }

        private List<Pizza> ChoosePizzas()
        {
            int option;
            List<Pizza> pizzas = new List<Pizza>();

            do
            {
                ShowMenu();

                option = InputPizzaNumber();
                int pizzaIndex = option - 1;

                if (pizzaIndex < Menu.Count)
                {
                    pizzas.Add(Menu[pizzaIndex]);
                }

            } while (option != Menu.Count + 1);

            return pizzas;
        }

        private int InputPizzaNumber()
        {
            do
            {
                if (Int32.TryParse(Console.ReadLine(), out int num))
                {
                    if (num > 0 && num <= Menu.Count + 1)
                    {
                        return num;
                    }                    
                }

                Console.WriteLine("Wrong number. Try again");
            } while (true);
        }

        private void ShowMenu()
        {
            for (int i = 0; i < Menu.Count; i++)
            {
                Console.WriteLine("{0} - {1}(Price: {2}; Cooking time: {3})",
                    i + 1,
                    Menu[i].Name,
                    Menu[i].Price,
                    Menu[i].CookingTime);
            }

            Console.WriteLine((Menu.Count + 1).ToString() + " - Exit");
        }
    }
}
