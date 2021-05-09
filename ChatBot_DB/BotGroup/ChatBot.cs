using System.Collections.Generic;
using static System.Console;


namespace ChatBot_DB
{
    sealed class ChatBot
    {

        public ChatBot()
        {
            //_userBase.baseChangedEvent += EventMethods.UserBaseChanged;
            //_sushiBase.baseChangedEvent += EventMethods.SushiBaseChanged;
        }

        public void MainMenu(UserAdmin admin)
        {
            while (true)
            {
                Clear();

                switch (ConsoleWork.Choose(new List<string>() { "Регистрация", "Вход-пользователь" }))
                {
                    case "Регистрация":
                        UserMenu(admin, admin.AddUserToUsersTable());
                        break;
                    case "Вход-пользователь":
                        UserMenu(admin, admin.GetUser());
                        break;
                }
            }
        }

        void UserMenu(UserAdmin admin, User user)
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
                        user.GetAllSushisInfo();
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

        void BinMenu(User user)
        {
            while (true)
            {
                Clear();

                switch (ConsoleWork.Choose(new List<string>() { "Просмотеть-корзину", "Добавить-суши", "Удалить-суши", "Назад" }))
                {
                    case "Просмотеть-корзину":
                        new BinDB() { TableId = user.BinId, SushiTableId = user.SushiTableID }.GetBinInfo(); 
                        break;
                    case "Добавить-суши":
                        user.AddItemToBin();
                        break;
                    case "Удалить-суши":
                        user.DeleteItemFromBin();
                        break;
                    case "Назад":
                        return;
                }
            }
        }

        void OrderMenu(User user)
        {
            while (true)
            {
                Clear();

                switch (ConsoleWork.Choose(new List<string>() { "Сформировать-заказ", "Просмотреть-заказ", "Оплатить-заказ", "Назад" }))
                {
                    case "Сформировать-заказ":
                        user.OpenOrder();
                        break;
                    case "Просмотреть-заказ":
                        new ArchiveDB() { TableId = user.ArchiveId }.GetOrderInfo(user.LastOrderId);
                        break;
                    case "Оплатить-заказ":       
                        user.PayOrder();
                        break;
                    case "Назад":
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
                        new SushiRacksDB() { TableId = guest.SushiRacksTableID }.GetSushiInfo();
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
