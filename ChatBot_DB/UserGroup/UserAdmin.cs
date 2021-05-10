using System;
using static System.Console;

namespace ChatBot_DB
{
    public sealed class UserAdmin : User
    {

        public void CreateSushiTable()
        {
            SushisTable sushis = new();
            sushis.CreateTable(Guid.NewGuid());
            SushiTableID = sushis.TableId;
        }

        public void AddSushiToSushiTable(Sushi sushi)
        {
            SushisTable sushis = new() {TableId = SushiTableID };
            sushis.CreateItem(sushi);
        }

        public void CreateSushiRacksTable()
        {
            SushiRacksTable sushiRacks = new();
            sushiRacks.SushiTableId = SushiTableID;
            sushiRacks.CreateTable(Guid.NewGuid());          
            SushiRacksTableID = sushiRacks.TableId;
        }

        public void AddSushiToSushiRacksTable(Sushi sushi, int amount)
        {
            SushisTable sushis = new() { TableId = SushiTableID };
            SushiRacksTable sushiRacks = new() { TableId = SushiRacksTableID };
            Sushi tempSushi = sushis?.ReadItem(sushi);
            sushiRacks.CreateItem(new() {Name = tempSushi?.Name, Amount = amount });
        }

        public User GetUser()
        {
            UsersTable users = new() { TableId = UsersTableID };
            User user = Registration.LogInUser(this);                      
            return user;
        }

        public void GetUsersInfo()
        {
            UsersTable users = new() { TableId = UsersTableID };
            users.GetAllItemsInfo();
        }

        public void CreateUsersTable()
        {
            UsersTable users = new();
            users.CreateTable(Guid.NewGuid());
            UsersTableID = users.TableId;
        }

        public User AddUserToUsersTable()
        {
            UsersTable users = new() { TableId = UsersTableID };
            User user = Registration.RegistrateUser(this);
            users.CreateItem(user);
            return user;
        }

        public void DeleteUser()
        {
            UsersTable users = new() { TableId = UsersTableID };
            User user = Registration.LogInUser(this);

            if (user != null)
            {
                WriteLine($"Пользователь {user.Name} удален.");
                users.DeleteItem(user);
            }
            ReadKey();
        }
    }
}
