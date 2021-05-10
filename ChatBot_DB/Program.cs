using System;

namespace ChatBot_DB
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.LogInfo("---Программа запущена---", new(""));
            Log.LogInfo("Все ОК", new(""));

            UserAdmin admin = new();
            admin.CreateUsersTable();

            admin.CreateSushiTable();
            admin.AddSushiToSushiTable(new() { Name = "Сяке-Маке", Price = 100, ID = Guid.NewGuid() });
            admin.AddSushiToSushiTable(new() { Name = "Гуро-Харакири", Price = 100, ID = Guid.NewGuid() });
            admin.AddSushiToSushiTable(new() { Name = "Фукусима-Глоу", Price = 100, ID = Guid.NewGuid() });

            admin.CreateSushiRacksTable();
            admin.AddSushiToSushiRacksTable(new() { Name = "Сяке-Маке", Price = 100 }, 9);
            admin.AddSushiToSushiRacksTable(new() { Name = "Гуро-Харакири", Price = 100 }, 9);
            admin.AddSushiToSushiRacksTable(new() { Name = "Фукусима-Глоу", Price = 100 }, 9);

            new ChatBot().MainMenu(admin);

            Log.LogInfo("---Программа остановлена---", new(""));
        }
    }
}
