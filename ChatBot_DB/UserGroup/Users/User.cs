using System;
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
            WriteLine($"Введи имя (Используй только буквы):");

            Validation.TryValidate(this, nameof(Name));

            new UsersDB().UpdateItem(this);
        }

        public void ChangePassword()
        {
            Clear();
            WriteLine("Введи пароль аккаунта (6-12 букв латинского алфавита или цифр):");

            Validation.TryValidate(this, nameof(Password));

            new UsersDB().UpdateItem(this);
        }

        public void ChangeMail()
        {
            Clear();
            UsersDB dB = new();

            WriteLine("Введи адрес электронной почты:");

            while (true)
            {
                Validation.TryValidate(this, nameof(Mail));

                if (dB.ReadItem(this) == null) { break; }

                WriteLine("Данный адрес электронной почты уже зарегистрирован. Попробуй другой:");
                ReadKey();
            }
            dB.UpdateItem(this);
        }

        public void PutMoney()
        {
            Clear();
            WriteLine($"На счету {Name} {Money} р");

            WriteLine($"Введи сумму для перевода:");

            Validation.TryValidate(this, nameof(LastTransaction));

            Money += LastTransaction;

            new UsersDB().UpdateItem(this);

            WriteLine($"Баланс {Name} составляет {Money} р");
            ReadKey();
        }

        public void PayOrder()
        {
            //if (OrderBase.GetLastOrder() is Order order)
            //{
            //   if (!order.Paid && order.PayOrder(this))
            //    {
            //        Money -= order.Price;
            //        return;
            //    }
            //    if (order.Paid)
            //    { WriteLine($"Последний заказ оплачен"); }

            //   ReadKey();
            //}
        }

        public void OpenOrder()
        {
            BinDB bin = new() { TableId = BinTableId };
            CreateOrderTable();
            OrderDB order = new() { TableId = BinTableId };
            order.WriteAllItems(bin.ReadAllItems());
            bin.EmptyBin();
        }

        public void CreateBinTable()
        {
            Guid id = Guid.NewGuid();
            BinDB bin = new() { TableId = id };
            BinTableId = id;
            bin.CreateTable(id);
        }

        public void CreateOrdersTable()
        {
            Guid id = Guid.NewGuid();
            OrdersDB orders = new() { TableId = id };
            OrdersTableId = id;
            orders.CreateTable(id);
        }

        public void CreateOrderTable()
        {
            Guid id = Guid.NewGuid();
            BinDB bin = new() { TableId = id };         
            bin.CreateTable(id);
            OrdersDB orders = new() { TableId = OrdersTableId };
            orders.C
        }
    }
}
