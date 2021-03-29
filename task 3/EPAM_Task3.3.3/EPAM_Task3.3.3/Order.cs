using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_Task3._3._3
{
    public class Order
    {
        public int OrderNumber { get; init; }
        public List<Pizza> Pizzas { get; init; }
        public int PreparingTime { get; set; }
        public int TotalPrice { get; init; }

        public Order(int orderNumber, List<Pizza> pizzas, int totalPrice)
        {
            OrderNumber = orderNumber;
            Pizzas = pizzas;
            PreparingTime = GetPreparingTime(Pizzas);
            TotalPrice = totalPrice;
        }

        private static int GetPreparingTime(List<Pizza> pizzas)
        {
            int totalTime = 0;

            for (int i = 0; i < pizzas.Count; i++)
            {
                totalTime += pizzas[i].CookingTime;
            }

            return totalTime;
        }
    }
}
