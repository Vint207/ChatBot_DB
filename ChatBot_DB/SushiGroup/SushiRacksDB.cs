using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace ChatBot_DB
{
    class SushiRacksTable : RacksTable
    {

        public void GetAllSushisInfo(User user)
        {
            TableId = user.SushiRacksTableID;
            SushiTableId = user.SushiTableID;

            List<Rack> items = ReadAllItems();

            Console.Clear();

            if (items != null)
            {
                Console.WriteLine("Товары в магазине:");

                foreach (var item in items)
                { item?.GetInfo(); }

                Console.WriteLine($"Стоимость товаров в магазине {GetPrice()} р.");

                Console.ReadKey();
                return;
            }
            Console.WriteLine("В магазине нет товаров");
            Console.ReadKey();
            return;
        }
    }
}
