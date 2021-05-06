using System;
using System.Runtime.CompilerServices;
using static System.Console;

namespace ChatBot_DB
{
    public delegate void BaseChangedEvent<in T, in T1>(T obj, T1 obj1, [CallerMemberName] string method = "");
    public delegate void BaseChangedEvent<in T>(T obj, [CallerMemberName] string method = "");

    public static class EventMethods
    {

        public static void SushiBaseChanged(Sushi sushi, User ProtoUser, [CallerMemberName] string method = "")
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

        public static void ProtoUserBaseChanged(User ProtoUser, [CallerMemberName] string method = "")
        {
            ForegroundColor = ConsoleColor.Green;
            switch (method)
            {
                case "AddItem":
                    WriteLine($"--Администратором добавлен пользователь {ProtoUser.Name}--");
                    break;
                case "DeleteItem":
                    WriteLine($"--Администратором удален пользователь {ProtoUser.Name}--");
                    break;
                case "GetItem":
                    WriteLine($"--Профиль пользователя {ProtoUser.Name} просмотрен--");
                    break;
                case "GetAllItemsInfo":
                    WriteLine($"--Список пользователей просмотрен--");
                    break;
            }
            ForegroundColor = ConsoleColor.White;
            ReadKey();
        }

        public static void BinBaseChanged(Sushi sushi, User ProtoUser, [CallerMemberName] string method = "")
        {
            if (sushi != null || method.Equals("GetAllItemsInfo"))
            {
                ForegroundColor = ConsoleColor.Green;
                switch (method)
                {
                    case "AddItem":
                        WriteLine($"--В корзину пользователя {ProtoUser.Name} добавлены суши {sushi.Name} - {sushi.Price} р--");
                        break;
                    case "DeleteItem":
                        WriteLine($"--Из корзины пользователя {ProtoUser.Name} удалены суши {sushi.Name} - {sushi.Price} р--");
                        break;
                    case "GetItem":
                        WriteLine($"--Суши {sushi.Name} просмотрены в корзине пользователя {ProtoUser.Name}--");
                        break;
                    case "GetAllItemsInfo":
                        WriteLine($"--Список суши в корзине пользователя {ProtoUser.Name} просмотрен--");
                        break;
                }
                ForegroundColor = ConsoleColor.White;
                ReadKey();
            }
        }

        public static void OrderBaseChanged(Order order, User ProtoUser, [CallerMemberName] string method = "")
        {
            ForegroundColor = ConsoleColor.Green;
            switch (method)
            {
                case "AddItem":
                    WriteLine($"--Пользователь {ProtoUser.Name} открыл заказ {order.OpenDate}--");
                    break;
            }
            ForegroundColor = ConsoleColor.White;
            ReadKey();
        }
    }
}
