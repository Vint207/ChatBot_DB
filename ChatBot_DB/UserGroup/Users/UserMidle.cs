using static System.Console;

namespace Chat_Bot
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
        }

        public void ChangePassword()
        {
            Clear();
            WriteLine("Введи пароль аккаунта (6-12 букв латинского алфавита или цифр):");

            Validation.TryValidate(this, nameof(Password));
        }

        public void ChangeMail(UserBase userBase)
        {
            Clear();
            WriteLine("Введи адрес электронной почты:");

            while (true)
            {
                Validation.TryValidate(this, nameof(Mail));

                if (userBase.GetItem(new UserMiddle() { Mail = Mail }) == null)
                { return; }

                WriteLine("Данный адрес электронной почты уже зарегистрирован. Попробуй другой:");
                ReadKey();
            }
        }

        public void PutMoney()
        {
            Clear();
            WriteLine($"На счету {Name} {Money} р");

            WriteLine($"Введи сумму для перевода:");

            Validation.TryValidate(this, nameof(LastTransaction));

            Money += LastTransaction;

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
