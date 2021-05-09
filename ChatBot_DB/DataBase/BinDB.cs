using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace ChatBot_DB
{
    public class BinDB : RacksDB
    {


        public void GetBinInfo()
        {
            List<Rack> items = ReadAllItems();

            Console.Clear();

            if (items != null)
            {
                Console.WriteLine("Товары в корзине:");

                foreach (var item in items)
                { item?.GetInfo(); }

                Console.WriteLine($"Стоимость товаров в корзине {GetPrice()} р.");

                Console.ReadKey();
                return;
            }
            Console.WriteLine("Корзина пуста");
            Console.ReadKey();
            return;
        }
    }
}
