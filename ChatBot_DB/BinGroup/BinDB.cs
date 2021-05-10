using static System.Console;
using System;
using System.Collections.Generic;

namespace ChatBot_DB
{
    public class BinDB : RacksTable
    {

        public void AddItemToBin(User user)
        {
            SushisTable sushis = new() { TableId = user.SushiTableID };
            SushiRacksTable sushiRacks = new() { TableId = user.SushiRacksTableID, SushiTableId = user.SushiTableID };
            TableId = user.BinId;
            SushiTableId = user.SushiTableID;
            
            List<string> sushiName = new();
            List<Rack> items;
            Rack sushiRack = new();
            Sushi sushi;

            while (true)
            {
                Clear();
                WriteLine($"{user.Name}, Выбери суши для добавления в корзину.");
                WriteLine();

                items = sushiRacks.ReadAllItems();

                if (items != null)
                {
                    sushiName.Clear();

                    foreach (var item in items)
                    { sushiName?.Add($"{item.Name}. Цена {sushis.ReadItem(new() { Name = item.Name }).Price} р. Количество {item.Amount} шт."); }

                    sushi = sushis.ReadItem(new() { Name = ConsoleWork.Choose(sushiName) });

                    sushiRack.Name = sushi.Name;

                    CreateItem(sushiRack);
                    sushiRacks.DeleteItem(sushiRack);

                    GetBinInfo(user);
                }
                if (sushiRacks.GetPrice() <= 0)
                {
                    Clear();
                    WriteLine($"В магазин не осталось суши");
                    ReadKey();
                    return;
                }
                WriteLine();
                WriteLine($"{user.Name}, хочешь заказать еще суши?");

                if (!ConsoleWork.Choose()) { return; }
            }
        }

        public void DeleteItemFromBin(User user)
        {
            SushisTable sushis = new() { TableId = user.SushiTableID };
            SushiRacksTable sushiRacks = new() { TableId = user.SushiRacksTableID, SushiTableId = user.SushiTableID };
            TableId = user.BinId;
            SushiTableId = user.SushiTableID;
           
            List<string> sushiName = new();
            List<Rack> items;
            Rack sushiRack = new() { Amount = 0 };
            Sushi sushi;

            while (true)
            {
                Clear();
                WriteLine($"{user.Name}, Выбери суши, который хочешь удалить из корзины:");
                WriteLine();

                items = ReadAllItems();

                if (items != null)
                {
                    sushiName.Clear();

                    foreach (var item in items)
                    { sushiName?.Add($"{item.Name}. Цена {sushis.ReadItem(new() { Name = item.Name }).Price} р. Количество {item.Amount} шт."); }

                    sushi = sushis.ReadItem(new() { Name = ConsoleWork.Choose(sushiName) });

                    sushiRack.Name = sushi.Name;
                    DeleteItem(sushiRack);
                    sushiRacks.CreateItem(sushiRack);                 
                }
                if (GetPrice() <= 0)
                {
                    Clear();
                    WriteLine($"Корзина пуста");
                    ReadKey();
                    return;
                }
                else 
                { GetBinInfo(user); }

                WriteLine();
                WriteLine($"{user.Name}, хочешь удалить еще суши?");

                if (!ConsoleWork.Choose()) { return; }
            }
        }

        public void GetBinInfo(User user)
        {
            TableId = user.BinId;
            SushiTableId = user.SushiTableID;

            List<Rack> items = ReadAllItems();

            Clear();

            if (items != null)
            {
                WriteLine($"Товары в корзине {user.Name}:");

                foreach (var item in items)
                { item?.GetInfo(); }

                WriteLine($"Стоимость товаров в корзине {GetPrice()} р.");

                ReadKey();
                return;
            }
            WriteLine("Корзина пуста");
            ReadKey();
            return;
        }
    }
}
