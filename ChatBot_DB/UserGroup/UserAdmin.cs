using System;
using static System.Console;

namespace ChatBot_DB
{
    public sealed class UserAdmin : User
    {

        public void CreateSushiTable()
        {
            SushisDB sushis = new();
            sushis.CreateTable(Guid.NewGuid());
            SushiTableID = sushis.TableId;
        }

        public void AddSushiToSushiTable(Sushi sushi)
        {
            SushisDB sushis = new() {TableId = SushiTableID };
            sushis.CreateItem(sushi);
        }

        public void CreateSushiRacksTable()
        {
            SushiRacksDB sushiRacks = new();
            sushiRacks.SushiTableId = SushiTableID;
            sushiRacks.CreateTable(Guid.NewGuid());          
            SushiRacksTableID = sushiRacks.TableId;
        }

        public void AddSushiToSushiRacksTable(Sushi sushi, int amount)
        {
            SushisDB sushis = new() { TableId = SushiTableID };
            SushiRacksDB sushiRacks = new() { TableId = SushiRacksTableID };
            Sushi tempSushi = sushis?.ReadItem(sushi);
            sushiRacks.CreateItem(new() {Name = tempSushi?.Name, Amount = amount });
        }

        public User GetUser()
        {
            UsersDB users = new() { TableId = UsersTableID };
            User user = Registration.LogInUser(UsersTableID);

            //if (user != null) { user.GetInfo(); }
            ReadKey();
            return user;
        }

        public void GetUsersInfo()
        {
            UsersDB users = new() { TableId = UsersTableID };
            users.GetAllItemsInfo();
        }

        public void CreateUsersTable()
        {
            UsersDB users = new();
            users.CreateTable(Guid.NewGuid());
            UsersTableID = users.TableId;
        }

        public User AddUserToUsersTable()
        {
            UsersDB users = new() { TableId = UsersTableID };
            User user = Registration.RegistrateUser(UsersTableID, SushiTableID, SushiRacksTableID);
            users.CreateItem(user);
            return user;
        }

        public void DeleteUser()
        {
            UsersDB users = new() { TableId = UsersTableID };
            User user = Registration.LogInUser(UsersTableID);

            if (user != null)
            {
                WriteLine($"Пользователь {user.Name} удален.");
                users.DeleteItem(user);
            }
            ReadKey();
        }
    }
}
