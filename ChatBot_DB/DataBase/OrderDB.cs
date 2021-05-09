using System;
using System.Collections.Generic;

namespace ChatBot_DB
{
    public class OrderDB : RacksDB
    {

        public DateTime OpenDate { get; set; }
        public DateTime CloseDate { get; set; }
        public double OrderPrice { get; set; }
        public bool Paid { get; set; }
        public bool Closed { get; set; }

        public void OpenOrder()
        {
            OpenDate = DateTime.Now;
        }

        public void PayOrder(double money)
        {
            Paid = true;
        }

        public void CloseOrder()
        {
            CloseDate = DateTime.Now;
            Closed = true;
        }

        public void GetOrderInfo()
        {
            List<Rack> items = ReadAllItems();
           
            Console.Clear();

            if (items != null)
            {
                Console.WriteLine("Последний заказ:");

                foreach (var item in items)
                { item?.GetInfo(); }

                Console.WriteLine($"Откыт: {OpenDate}");
              
                if (CloseDate!=default)
                {Console.WriteLine($"Закрыт: {CloseDate}");}
                Console.WriteLine($"Не закрыт");

                if (Paid)
                { Console.WriteLine($"Оплачен");}
                Console.WriteLine($"Не оплачен");

                Console.ReadKey();
                return;
            }
            Console.WriteLine("Заказов нет");
            Console.ReadKey();
            return;
        }
    }
}
