using System.Collections.Generic;
using static System.Console;

namespace ChatBot_DB
{
    public class BinDB : RacksTable
    {

        public void AddItemToBin(User user)
        {
            TableId = user.BinId;
            SushiTableId = user.SushiTableID;
            SushisTable sushis = new() { TableId = user.SushiTableID };
            SushiRacksTable sushiRacks = new() { TableId = user.SushiRacksTableID, SushiTableId = user.SushiTableID }; 
            List<string> sushiNames = new();
            List<Rack> items;
            Rack sushiRack = new();
            Rack tempRack;
            Sushi sushi;

            while (true)
            {
                Clear();
                WriteLine($"{user.Name}, Выбери суши для добавления в корзину:");

                items = sushiRacks.ReadAllItems();

                if (items != null)
                {
                    sushiNames.Clear();

                    foreach (var item in items)
                    { sushiNames?.Add($"{item.Name}. Цена {sushis.ReadItem(new() { Name = item.Name }).Price} р. Количество {item.Amount} шт."); }

                    sushi = sushis.ReadItem(new() { Name = ConsoleWork.Choose(sushiNames) });

                    sushiRack.Name = sushi.Name;

                    tempRack = ReadItem(sushiRack);

                    if (tempRack == null) 
                    {
                        CreateItem(sushiRack);
                    }
                    else
                    {
                        tempRack.Amount++;
                        UpdateItem(tempRack);
                    }
                    tempRack = sushiRacks.ReadItem(sushiRack);

                    if (tempRack.Amount > 1) 
                    {
                        tempRack.Amount--;
                        sushiRacks.UpdateItem(tempRack);
                    }
                    else
                    {
                        sushiRacks.DeleteItem(tempRack);
                    }
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
            TableId = user.BinId;
            SushiTableId = user.SushiTableID;
            SushisTable sushis = new() { TableId = user.SushiTableID };
            SushiRacksTable sushiRacks = new() { TableId = user.SushiRacksTableID, SushiTableId = user.SushiTableID };      
            List<string> sushiNames = new();
            List<Rack> items;
            Rack sushiRack = new() { Amount = 1 };
            Rack tempRack;
            Sushi sushi;

            while (true)
            {
                Clear();
                WriteLine($"{user.Name}, Выбери суши, который хочешь удалить из корзины:");
                WriteLine();

                items = ReadAllItems();

                if (items != null)
                {
                    sushiNames.Clear();

                    foreach (var item in items)
                    { sushiNames?.Add($"{item.Name}. Цена {sushis.ReadItem(new() { Name = item.Name }).Price} р. Количество {item.Amount} шт."); }

                    sushi = sushis.ReadItem(new() { Name = ConsoleWork.Choose(sushiNames) });

                    sushiRack.Name = sushi.Name;
                   
                    tempRack = sushiRacks.ReadItem(sushiRack);

                    if (tempRack == null)
                    {
                        sushiRacks.CreateItem(sushiRack);
                    }
                    else
                    {
                        tempRack.Amount++;
                        sushiRacks.UpdateItem(tempRack);
                    }
                    tempRack = ReadItem(sushiRack);

                    if (tempRack.Amount > 1)
                    {
                        tempRack.Amount--;
                        UpdateItem(tempRack);
                    }
                    else
                    {
                        DeleteItem(tempRack);
                    }
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
