using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_Task3._3._3
{
    public class Pizzeria
    {
        public string Name { get; }
        public List<Pizza> Menu { get; private set; }
        public int CurrentOrder { get; private set; }
        public List<Order> ProcessingOrders { get; private set; }
        public List<Order> ReadyToTakeOrders { get; private set; }

        public Pizzeria(string name, List<Pizza> menu)
        {
            Name = name;
            Menu = menu;
            CurrentOrder = 0;
            ProcessingOrders = new List<Order>();
            ReadyToTakeOrders = new List<Order>();
        }

        // 0 - all ok
        // -1 - order is empty
        // -2 - not enough funds
        public int MakeOrder(Client client)
        {
            List<Pizza> pizzasForOrder = ChoosePizzas();

            if (pizzasForOrder.Count == 0)
            {
                return -1;
            }

            int totalPrice = GetPrice(pizzasForOrder);

            if (totalPrice > client.Balance)
            {
                return -2;
            }

            CurrentOrder++;
            Order orderForPizzeria = new Order(CurrentOrder, pizzasForOrder, totalPrice);
            Order orderForClient = new Order(CurrentOrder, pizzasForOrder, totalPrice);

            client.Balance -= totalPrice;
            client.Orders.Add(orderForClient);

            ProcessingOrders.Add(orderForPizzeria);

            return 0;
        }

        public void WorkingProcess(int timeSpend)
        {
            for (int i = 0; i < ProcessingOrders.Count; i++)
            {
                if (ProcessingOrders[i].PreparingTime > timeSpend)
                {
                    ProcessingOrders[i].PreparingTime -= timeSpend;
                }

                else
                {
                    ProcessingOrders[i].PreparingTime = 0;
                    ReadyToTakeOrders.Add(ProcessingOrders[i]);
                    ProcessingOrders.RemoveAt(i);

                    i--;
                }
            }
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
