using System;
using System.Runtime.CompilerServices;
using static System.Console;

namespace ChatBot_DB
{
    public delegate void BaseChangedEvent<in T, in T1>(T obj, T1 obj1, [CallerMemberName] string method = "");
    public delegate void BaseChangedEvent<in T>(T obj, [CallerMemberName] string method = "");

    public static class EventMethods
    {

        public static void SushiBaseChanged(Sushi sushi, UserGuest user, [CallerMemberName] string method = "")
        {
            if (sushi != null || method.Equals("GetAllItemsInfo"))
            {
                ForegroundColor = ConsoleColor.Green;
                switch (method)
                {
                    case "AddItem":
                        WriteLine($"--В базу добавлены суши {sushi.Name} - {sushi.Price} р--");
                        break;
                    case "DeleteItem":
                        WriteLine($"--Из базы удалены суши {sushi.Name} - {sushi.Price} р--");
                        break;
                    case "GetItem":
                        WriteLine($"--Данные о суши {sushi.Name} просмотрены в базе--");
                        break;
                    case "GetAllItemsInfo":
                        WriteLine($"--Список суши в базе просмотрен--");
                        break;
                }
                ForegroundColor = ConsoleColor.White;
                ReadKey();
            }
        }

        public static void UserBaseChanged(UserGuest user, [CallerMemberName] string method = "")
        {
            ForegroundColor = ConsoleColor.Green;
            switch (method)
            {
                case "AddItem":
                    WriteLine($"--Администратором добавлен пользователь {user.Name}--");
                    break;
                case "DeleteItem":
                    WriteLine($"--Администратором удален пользователь {user.Name}--");
                    break;
                case "GetItem":
                    WriteLine($"--Профиль пользователя {user.Name} просмотрен--");
                    break;
                case "GetAllItemsInfo":
                    WriteLine($"--Список пользователей просмотрен--");
                    break;
            }
            ForegroundColor = ConsoleColor.White;
            ReadKey();
        }

        public static void BinBaseChanged(Sushi sushi, UserGuest user, [CallerMemberName] string method = "")
        {
            if (sushi != null || method.Equals("GetAllItemsInfo"))
            {
                ForegroundColor = ConsoleColor.Green;
                switch (method)
                {
                    case "AddItem":
                        WriteLine($"--В корзину пользователя {user.Name} добавлены суши {sushi.Name} - {sushi.Price} р--");
                        break;
                    case "DeleteItem":
                        WriteLine($"--Из корзины пользователя {user.Name} удалены суши {sushi.Name} - {sushi.Price} р--");
                        break;
                    case "GetItem":
                        WriteLine($"--Суши {sushi.Name} просмотрены в корзине пользователя {user.Name}--");
                        break;
                    case "GetAllItemsInfo":
                        WriteLine($"--Список суши в корзине пользователя {user.Name} просмотрен--");
                        break;
                }
                ForegroundColor = ConsoleColor.White;
                ReadKey();
            }
        }

        public static void OrderBaseChanged(Order order, UserGuest user, [CallerMemberName] string method = "")
        {
            ForegroundColor = ConsoleColor.Green;
            switch (method)
            {
                case "AddItem":
                    WriteLine($"--Пользователь {user.Name} открыл заказ {order.OpenDate}--");
                    break;
            }
            ForegroundColor = ConsoleColor.White;
            ReadKey();
        }
    }
}
