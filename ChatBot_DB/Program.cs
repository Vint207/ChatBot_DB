using System;

namespace ChatBot_DB
{
    class Program
    {
        static void Main(string[] args)
        {
            UserDB db = new();

            //await usersMSSql.CreateItem(new() { Name = "ДодикВася", Password = "123456", Mail = "dodik123@mail.tg" });

            //UserMiddle user;

            //user =  db.ReadItem(new() { Mail = "dodik123@mail.tg" });

            // db.DeleteItem(new() {Mail = "a@a.aa" });

            SushiDB sushiDB = new();

            Sushi sushi = new(Guid.NewGuid()) { Name = "Сяке-Маке", Price = 100 };
            sushiDB.CreateItem(sushi);
            sushi = new(Guid.NewGuid()) { Name = "Гуро-Харакири", Price = 100 };
            sushiDB.CreateItem(sushi);
            sushi = new(Guid.NewGuid()) { Name = "Фукусима-Глоу", Price = 100 };
            sushiDB.CreateItem(sushi);

            SushiRackDB sushirackDB = new();

            SushiRack sushiRack = new() { Name = "Сяке-Маке", Amount = 99 };
            sushirackDB.CreateItem(sushiRack);
            sushiRack = new() { Name = "Гуро-Харакири", Amount = 99 };
            sushirackDB.CreateItem(sushiRack);
            sushiRack = new() { Name = "Фукусима-Глоу", Amount = 99 };
            sushirackDB.CreateItem(sushiRack);
            sushirackDB.CreateItem(sushiRack);

            Sushi sushi1 = sushiDB.ReadItem(new() { Name = "Фукусима-Глоу" });

            if (sushi1 != null)
            {
                sushiDB.UpdateItem(new() {Name = "Хонда-Ролл", ID = sushi1.ID });
            }



            //await db.UpdateItem(user);

            ////UserBase users = new();
            //SushiBase sushi = new();

            //new ChatBot(users, sushi).MainMenu();

            //if (connection.State == ConnectionState.Open)
            //{
            //    await connection.CloseAsync();
            //    Console.WriteLine("Подключение закрыто...");
            //}
        }
    }
}
