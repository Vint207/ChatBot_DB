using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace ChatBot_DB
{
    public class User : UserGuest
    {

        public User()
        {
            //Bin.baseChangedEvent += EventMethods.BinBaseChanged;
            //OrderBase.baseChangedEvent += EventMethods.OrderBaseChanged;
        }

        public void ChangeName()
        {
            Clear();
            UsersDB users = new() { TableId = UsersTableID };

            WriteLine($"Введи имя (Используй только буквы):");

            Validation.TryValidate(this, nameof(Name));

            users.UpdateItem(this);
        }

        public void ChangePassword()
        {
            Clear();
            UsersDB users = new() { TableId = UsersTableID };

            WriteLine("Введи пароль аккаунта (6-12 букв латинского алфавита или цифр):");

            Validation.TryValidate(this, nameof(Password));

            users.UpdateItem(this);
        }

        public void ChangeMail()
        {
            Clear();
            UsersDB users = new() { TableId = UsersTableID };

            WriteLine("Введи адрес электронной почты:");

            while (true)
            {
                Validation.TryValidate(this, nameof(Mail));

                if (users.ReadItem(this) == null) { break; }

                WriteLine("Данный адрес электронной почты уже зарегистрирован. Попробуй другой:");
                ReadKey();
            }
            users.UpdateItem(this);
        }

        public User CreateUser()
        {
            Clear();
            UsersDB users = new() { TableId = UsersTableID };

            WriteLine("Введи адрес электронной почты:");

            while (true)
            {
                Validation.TryValidate(this, nameof(Mail));

                if (users.ReadItem(this) == null) { break; }

                WriteLine("Данный адрес электронной почты уже зарегистрирован. Попробуй другой:");
                ReadKey();
            }
            Clear();
            WriteLine($"Введи имя (Используй только буквы):");
            Validation.TryValidate(this, nameof(Name));

            Clear();
            WriteLine("Введи пароль аккаунта (6-12 букв латинского алфавита или цифр):");
            Validation.TryValidate(this, nameof(Password));

            return this;
        }

        public void PutMoney()
        {
            Clear();
            UsersDB users = new() { TableId = UsersTableID };

            WriteLine($"На счету {Name} {Money} р");

            WriteLine($"Введи сумму для перевода:");

            Validation.TryValidate(this, nameof(LastTransaction));

            Money += LastTransaction;

            users.UpdateItem(this);

            WriteLine($"Баланс {Name} составляет {Money} р");
            ReadKey();
        }

        public void GetAllSushisInfo()
        {
            Clear();
            SushisDB sushis = new() { TableId = SushiTableID };
            sushis.GetAllItemsInfo();
            ReadKey();
        }

        public void OpenOrder()
        {
            Clear();
            BinDB bin = new() { TableId = BinId, SushiTableId = SushiTableID };

            if (bin.GetPrice() > 0)
            {
                ArchiveDB archive = new() { TableId = ArchiveId };
                OrderDB order = new() { SushiTableId = SushiTableID };
                UsersDB users = new() { TableId = UsersTableID };
                order.CreateTable(Guid.NewGuid());
                order.OpenOrder();
                order.WriteAllItems(bin.ReadAllItems());
                order.OrderPrice = bin.GetPrice();
                archive.CreateItem(order);
                LastOrderId = order.TableId;
                users.UpdateItem(this);
                bin.ClearTable();
                WriteLine("Заказ сформирован");
                ReadKey();
                return;
            }
            WriteLine("Заказ не сформирован. Корзина пуста");
            ReadKey();
        }

        public void PayOrder()
        {
            Clear();

            if (LastOrderId != default)
            {
                ArchiveDB archive = new() { TableId = ArchiveId };
                OrderDB order = archive.ReadItem(new() { TableId = LastOrderId, SushiTableId = SushiTableID });

                if (!order.Paid)
                {
                    if (order.OrderPrice <= Money)
                    {
                        order.PayOrder(Money);
                        archive.UpdateItem(order);
                        Money -= order.OrderPrice;
                        WriteLine($"Заказ оплачен");
                        OrderDelay();
                        ReadKey();
                        return;
                    }
                    WriteLine($"На счету недостаточно средств");
                    ReadKey();
                    return;
                }
                WriteLine($"Последний заказ оплачен");
                ReadKey();
                return;
            }
            WriteLine($"Нет заказов");
            ReadKey();
        }

        async void OrderDelay() =>
            await Task.Run(() => WaitTime());

        void WaitTime()
        {
            Thread.Sleep(10000);
            CloseOrder();
        }

        public void CloseOrder()
        {
            ArchiveDB archive = new() { TableId = ArchiveId };
            OrderDB order = archive.ReadItem(new() { TableId = LastOrderId });
            order.CloseOrder();
            archive.UpdateItem(order);
        }

        public void AddItemToBin()
        {
            SushisDB sushis = new() { TableId = SushiTableID };
            SushiRacksDB sushiRacks = new() { TableId = SushiRacksTableID, SushiTableId = SushiTableID };
            BinDB bin = new() { TableId = BinId, SushiTableId = SushiTableID };
            List<string> sushiName = new();
            List<Rack> items;
            Rack sushiRack = new();
            Sushi sushi;

            while (true)
            {
                Clear();
                WriteLine($"{Name}, Выбери суши для добавления в корзину.");
                WriteLine();

                items = sushiRacks.ReadAllItems();

                if (items != null)
                {
                    sushiName.Clear();

                    foreach (var item in items)
                    { sushiName?.Add($"{item.Name}. Цена {sushis.ReadItem(new() { Name = item.Name }).Price} р. Количество {item.Amount} шт."); }

                    sushi = sushis.ReadItem(new() { Name = ConsoleWork.Choose(sushiName) });

                    sushiRack.Name = sushi.Name;

                    bin.CreateItem(sushiRack);
                    sushiRacks.DeleteItem(sushiRack);

                    bin.GetBinInfo();
                }
                if (sushiRacks.GetPrice() <= 0)
                {
                    Clear();
                    WriteLine($"В магазин не осталось суши");
                    ReadKey();
                    return;
                }
                WriteLine();
                WriteLine($"{Name}, хочешь заказать еще суши?");

                if (!ConsoleWork.Choose()) { return; }
            }
        }

        public void DeleteItemFromBin()
        {
            SushisDB sushis = new() { TableId = SushiTableID };
            SushiRacksDB sushiRacks = new() { TableId = SushiRacksTableID, SushiTableId = SushiTableID };
            BinDB bin = new() { TableId = BinId, SushiTableId = SushiTableID };
            List<string> sushiName = new();
            List<Rack> items;
            Rack sushiRack = new() { Amount = 0 };
            Sushi sushi;

            while (true)
            {
                Clear();
                WriteLine($"{Name}, Выбери суши, который хочешь удалить из корзины:");
                WriteLine();

                items = bin.ReadAllItems();

                if (items != null)
                {
                    sushiName.Clear();

                    foreach (var item in items)
                    { sushiName?.Add($"{item.Name}. Цена {sushis.ReadItem(new() { Name = item.Name }).Price} р. Количество {item.Amount} шт."); }

                    sushi = sushis.ReadItem(new() { Name = ConsoleWork.Choose(sushiName) });

                    sushiRack.Name = sushi.Name;
                    bin.DeleteItem(sushiRack);
                    sushiRacks.CreateItem(sushiRack);

                    bin.GetBinInfo();
                }
                if (bin.GetPrice() <= 0)
                {
                    Clear();
                    WriteLine($"Корзина пуста");
                    ReadKey();
                    return;
                }

                WriteLine();
                WriteLine($"{Name}, хочешь удалить еще суши?");

                if (!ConsoleWork.Choose()) { return; }
            }
        }

        //public void GetItemsInfoFromBin()
        //{
        //    Clear();
        //    BinDB bin = new() { TableId = BinId, SushiTableId = SushiTableID };
        //    double binPrice = bin.GetPrice();

        //    if (binPrice > 0)
        //    {
        //        WriteLine("Состав корзины");
        //        bin.GetAllItemsInfo();
        //        WriteLine($"Стоимость товаров в корзине {binPrice} р.");
        //        ReadKey();
        //        return;
        //    }
        //    WriteLine("Корзина пуста");
        //    ReadKey();
        //}

        public void CreateUserBinTable()
        {
            BinDB bin = new();
            bin.CreateTable(Guid.NewGuid());
            BinId = bin.TableId;
        }

        public void CreateUserArchiveTable()
        {
            ArchiveDB archive = new();
            archive.CreateTable(Guid.NewGuid());
            ArchiveId = archive.TableId;
        }
    }
}
