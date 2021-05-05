using static System.Console;

namespace ChatBot_DB
{
    public class UserMiddle : UserGuest
    {

        public Bin Bin;
        public OrderBase OrderBase;

        public UserMiddle()
        {
            Bin = new();
            OrderBase = new();
            Bin.baseChangedEvent += EventMethods.BinBaseChanged;
            OrderBase.baseChangedEvent += EventMethods.OrderBaseChanged;
        }

        public void ChangeName()
        {
            Clear();
            WriteLine($"Введи имя (Используй только буквы):");

            Validation.TryValidate(this, nameof(Name));

            new UserDB().UpdateItem(this);
        }

        public void ChangePassword()
        {
            Clear();
            WriteLine("Введи пароль аккаунта (6-12 букв латинского алфавита или цифр):");

            Validation.TryValidate(this, nameof(Password));

            new UserDB().UpdateItem(this);
        }

        public void ChangeMail()
        {
            Clear();
            UserDB dB = new();

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

            new UserDB().UpdateItem(this);

            WriteLine($"Баланс {Name} составляет {Money} р");
            ReadKey();
        }

        public void PayOrder()
        {
            if (OrderBase.GetLastOrder() is Order order)
            {
                if (!order.Paid && order.PayOrder(this))
                {
                    Money -= order.Price;
                    return;
                }
                if (order.Paid)
                { WriteLine($"Последний заказ оплачен"); }

                ReadKey();
            }
        }
    }
}
