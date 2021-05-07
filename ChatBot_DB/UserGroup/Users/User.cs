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

        public void ChangeName(Guid usersTableId)
        {
            Clear();
            UsersDB users = new() { TableId = usersTableId };

            WriteLine($"Введи имя (Используй только буквы):");

            Validation.TryValidate(this, nameof(Name));

            users.UpdateItem(this);
        }

        public void ChangePassword(Guid usersTableId)
        {
            Clear();
            UsersDB users = new() { TableId = usersTableId };

            WriteLine("Введи пароль аккаунта (6-12 букв латинского алфавита или цифр):");

            Validation.TryValidate(this, nameof(Password));

            users.UpdateItem(this);
        }

        public void ChangeMail(Guid usersTableId)
        {
            Clear();
            UsersDB users = new() { TableId = usersTableId };

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

        public void PutMoney(Guid usersTableId)
        {
            Clear();
            UsersDB users = new() { TableId = usersTableId };

            WriteLine($"На счету {Name} {Money} р");

            WriteLine($"Введи сумму для перевода:");

            Validation.TryValidate(this, nameof(LastTransaction));

            Money += LastTransaction;

            users.UpdateItem(this);

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
    }
}
