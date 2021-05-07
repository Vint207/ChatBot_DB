using System;

namespace ChatBot_DB
{
    class Program
    {
        static void Main(string[] args)
        {
            //usersDB.CreateItem(new() { Name = "Вася", Password = "123456", Mail = "vasya@a.aa", UserID = Guid.NewGuid() });
            //usersDB.CreateItem(new() { Name = "Петя", Password = "123456", Mail = "petya@a.aa", UserID = Guid.NewGuid() });
            //usersDB.CreateItem(new() { Name = "Миша", Password = "123456", Mail = "misha@a.aa", UserID = Guid.NewGuid() });
            //usersDB.CreateItem(new() { Name = "Саша", Password = "123456", Mail = "sasha@a.aa", UserID = Guid.NewGuid() });


            UserAdmin admin = new();
            admin.CreateUsersTable();

            admin.CreateSushiTable();
            admin.AddSushiToSushiTable(new() { Name = "Сяке-Маке", Price = 100, ID = Guid.NewGuid() });
            admin.AddSushiToSushiTable(new() { Name = "Гуро-Харакири", Price = 100, ID = Guid.NewGuid() });
            admin.AddSushiToSushiTable(new() { Name = "Фукусима-Глоу", Price = 100, ID = Guid.NewGuid() });

            admin.CreateSushiRacksTable();
            admin.AddSushiToSushiRacksTable(new() { Name = "Сяке-Маке", Price = 100 }, 99);
            admin.AddSushiToSushiRacksTable(new() { Name = "Гуро-Харакири", Price = 100 }, 99);
            admin.AddSushiToSushiRacksTable(new() { Name = "Фукусима-Глоу", Price = 100 }, 99);

            new ChatBot().MainMenu(admin);
        }
    }
}
