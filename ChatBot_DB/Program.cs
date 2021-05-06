using System;

namespace ChatBot_DB
{
    class Program
    {
        static void Main(string[] args)
        {
            UsersDB usersDB = new();
            SushisDB sushisDB = new();
            usersDB.CreateTable(Guid.NewGuid());
            sushisDB.CreateTable(Guid.NewGuid());

            SushiRacksDB sushiRacksDB = new() { SushiTableId = sushisDB.TableId };

            sushiRacksDB.CreateTable(Guid.NewGuid());

            usersDB.CreateItem(new() { Name = "Вася", Password = "123456", Mail = "vasya@a.aa", UserID = Guid.NewGuid() });
            usersDB.CreateItem(new() { Name = "Петя", Password = "123456", Mail = "petya@a.aa", UserID = Guid.NewGuid() });
            usersDB.CreateItem(new() { Name = "Миша", Password = "123456", Mail = "misha@a.aa", UserID = Guid.NewGuid() });
            usersDB.CreateItem(new() { Name = "Саша", Password = "123456", Mail = "sasha@a.aa", UserID = Guid.NewGuid() });


            usersDB.GetAllItemsInfo();


            sushisDB.CreateItem(new() { Name = "Сяке-Маке", Price = 100 });
            sushisDB.CreateItem(new() { Name = "Гуро-Харакири", Price = 100 });
            sushisDB.CreateItem(new() { Name = "Фукусима-Глоу", Price = 100 });


            sushisDB.GetAllItemsInfo();


            User user = usersDB.ReadItem(new() { Mail = "sasha@a.aa" });

            user.GetInfo();

            Console.ReadKey();

            user.CreateBinTable();
            user.CreateOrdersTable();
            user.OpenOrder();


            sushiRacksDB.DropTable();
            //binDB.DropTable();
            //orderDB.DropTable();
            //ordersDB.DropTable();
            usersDB.DropTable();
            sushisDB.DropTable();



            //user =  db.ReadItem(new() { Mail = "dodik123@mail.tg" });

            // db.DeleteItem(new() {Mail = "a@a.aa" });

            //SushiDB sushiDB = new();

            //Sushi sushi = new(Guid.NewGuid()) { Name = "Сяке-Маке", Price = 100 };
            //sushiDB.CreateItem(sushi);
            //sushi = new(Guid.NewGuid()) { Name = "Гуро-Харакири", Price = 100 };
            //sushiDB.CreateItem(sushi);
            //sushi = new(Guid.NewGuid()) { Name = "Фукусима-Глоу", Price = 100 };
            //sushiDB.CreateItem(sushi);

            //SushiRackDB sushirackDB = new();

            //SushiRack sushiRack = new() { Name = "Сяке-Маке", Amount = 99 };
            //sushirackDB.CreateItem(sushiRack);
            //sushiRack = new() { Name = "Гуро-Харакири", Amount = 99 };
            //sushirackDB.CreateItem(sushiRack);
            //sushiRack = new() { Name = "Фукусима-Глоу", Amount = 99 };
            //sushirackDB.CreateItem(sushiRack);
            //sushirackDB.CreateItem(sushiRack);

            //Sushi sushi1 = sushiDB.ReadItem(new() { Name = "Фукусима-Глоу" });

            //if (sushi1 != null)
            //{
            //    sushiDB.UpdateItem(new() {Name = "Хонда-Ролл", ID = sushi1.ID, Price = 100 });
            //}


            //ProtoUser user = db.ReadItem(new() { Mail = "dodik123@mail.tg" });

            //user.GetInfo();


            Console.ReadKey();

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
