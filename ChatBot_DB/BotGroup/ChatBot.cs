using System.Collections.Generic;
using static System.Console;


namespace ChatBot_DB
{
    sealed class ChatBot
    {

        public void MainMenu(UserAdmin admin)
        {
            while (true)
            {
                Clear();

                switch (ConsoleWork.Choose(new List<string>() { "Регистрация", "Вход-пользователь" }))
                {
                    case "Регистрация":
                        UserMenu(admin.AddUserToUsersTable());
                        break;
                    case "Вход-пользователь":
                        UserMenu(admin.GetUser());
                        break;
                }
            }
        }

        void UserMenu(User user)
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
                        new SushiRacksTable().GetAllSushisInfo(user); 
                        break;
                    case "Меню-корзины":
                        BinMenu(new(), user);
                        break;
                    case "Меню-заказа":
                        OrderMenu(new(), user);
                        break;
                    case "Выйти":
                        return;
                }
            }
        }

        void ProfileMenu(User user)
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

        void BinMenu(BinDB bin, User user)
        {
            while (true)
            {
                Clear();

                switch (ConsoleWork.Choose(new List<string>() { "Просмотеть-корзину", "Добавить-суши", "Удалить-суши", "Назад" }))
                {
                    case "Просмотеть-корзину":
                        bin.GetBinInfo(user); 
                        break;
                    case "Добавить-суши":
                        bin.AddItemToBin(user);
                        break;
                    case "Удалить-суши":
                        bin.DeleteItemFromBin(user);
                        break;
                    case "Назад":
                        return;
                }
            }
        }

        void OrderMenu(OrderTable order, User user)
        {
            while (true)
            {
                Clear();

                switch (ConsoleWork.Choose(new List<string>() { "Сформировать-заказ", "Просмотреть-заказ", "Оплатить-заказ", "Назад" }))
                {
                    case "Сформировать-заказ":
                        order.OpenOrder(user);
                        break;
                    case "Просмотреть-заказ":
                        order.GetOrderInfo(user);
                        break;
                    case "Оплатить-заказ":
                        order.PayOrder(user);
                        break;
                    case "Назад":
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
                        admin.GetUsersInfo();
                        break;
                    case "Просмотреть-пользователя":
                        admin.GetUser().GetInfo();
                        break;
                    case "Удалить-пользователя":
                        admin.DeleteUser();
                        break;
                    case "Выйти":
                        return;
                }
            }
        }
    }
}
