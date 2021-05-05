using System.Collections.Generic;
using static System.Console;


namespace ChatBot_DB
{
    sealed class ChatBot
    {
        UserBase _userBase;
        SushiBase _sushiBase;

        public ChatBot(UserBase userBase, SushiBase sushiBase)
        {
            _userBase = userBase;
            _sushiBase = sushiBase;
            _userBase.baseChangedEvent += EventMethods.UserBaseChanged;
            _sushiBase.baseChangedEvent += EventMethods.SushiBaseChanged;
        }

        public void MainMenu()
        {
            while (true)
            {
                Clear();

                switch (ConsoleWork.Choose(new List<string>() { "Регистрация", "Вход-пользователь", "Вход-гость", "Вход-администратор" }))
                {
                    case "Регистрация":
                        UserMenu(Registration.RegistrateUser(_userBase));
                        break;
                    case "Вход-пользователь":
                        UserMenu(Registration.LogInUser(_userBase));
                        break;
                    case "Вход-гость":
                        GuestMenu(new());
                        break;
                    case "Вход-администратор":
                        AdminMenu(Registration.LogInAdmin(_userBase));
                        break;
                }
            }
        }

        void UserMenu(UserMiddle user)
        {
            if (user == null) return;

            while (true)
            {
                Clear();

                switch (ConsoleWork.Choose(new List<string>() { "Просмотреть-аккаунт", "Настроить-аккаунт", "Пополнить-счет", "Меню-суши", "Меню-корзины", "Меню-заказа", "Выйти" }))
                {
                    case "Просмотреть-аккаунт":
                        user.GetInfo();
                        break;
                    case "Настроить-аккаунт":
                        ProfileMenu(user);
                        break;
                    case "Пополнить-счет":
                        user.PutMoney();
                        break;
                    case "Меню-суши":
                        _sushiBase.GetAllItemsInfo(user);
                        break;
                    case "Меню-корзины":
                        BinMenu(user);
                        break;
                    case "Меню-заказа":
                        OrderMenu(user);
                        break;
                    case "Выйти":
                        return;
                }
            }
        }

        void GuestMenu(UserGuest guest)
        {
            if (guest == null) return;

            while (true)
            {
                Clear();

                switch (ConsoleWork.Choose(new List<string>() { "Меню-суши", "Выйти" }))
                {
                    case "Меню-суши":
                        _sushiBase.GetAllItemsInfo(new());
                        break;
                    case "Выйти":
                        return;
                }
            }
        }

        void AdminMenu(UserAdmin admin)
        {
            if (admin == null) return;

            while (true)
            {
                Clear();

                switch (ConsoleWork.Choose(new List<string>() { "Просмотреть-пользователей", "Просмотреть-пользователя", "Удалить-пользователя", "Выйти" }))
                {
                    case "Просмотреть-пользователей":
                        admin.AllUsersInfo(_userBase);
                        break;
                    case "Просмотреть-пользователя":
                        admin.FindUserInfo(_userBase);
                        break;
                    case "Удалить-пользователя":
                        admin.DeleteUser(_userBase);
                        break;
                    case "Выйти":
                        return;
                }
            }
        }

        void ProfileMenu(UserMiddle user)
        {
            while (true)
            {
                Clear();

                switch (ConsoleWork.Choose(new List<string>() { "Изменить-имя", "Изменить-пароль", "Изменить-почту", "Назад" }))
                {
                    case "Изменить-имя":
                        user.ChangeName();
                        break;
                    case "Изменить-пароль":
                        user.ChangePassword();
                        break;
                    case "Изменить-почту":
                        user.ChangeMail();
                        break;
                    case "Назад":
                        return;
                }
            }
        }

        void BinMenu(UserMiddle user)
        {
            while (true)
            {
                Clear();

                switch (ConsoleWork.Choose(new List<string>() { "Просмотеть-корзину", "Добавить-суши", "Удалить-суши", "Назад" }))
                {
                    case "Просмотеть-корзину":
                        user.Bin.GetAllItemsInfo(user);
                        break;
                    case "Добавить-суши":
                        user.Bin.AddItemToBin(_sushiBase, user);
                        break;
                    case "Удалить-суши":
                        user.Bin.DeleteItemFromBin(_sushiBase, user);
                        break;
                    case "Назад":
                        return;
                }
            }
        }

        void OrderMenu(UserMiddle user)
        {
            while (true)
            {
                Clear();

                switch (ConsoleWork.Choose(new List<string>() { "Сформировать-заказ", "Просмотреть-заказ", "Оплатить-заказ", "Назад" }))
                {
                    case "Сформировать-заказ":
                        user.OrderBase.AddOrder(user);
                        break;
                    case "Просмотреть-заказ":
                        user.OrderBase?.GetLastOrder()?.GetInfo();
                        break;
                    case "Оплатить-заказ":
                        user.PayOrder();
                        break;
                    case "Назад":
                        return;
                }
            }
        }
    }
}
