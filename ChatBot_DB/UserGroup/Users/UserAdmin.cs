using System;
using static System.Console;

namespace ChatBot_DB
{
    public sealed class UserAdmin : User
    {

        public Guid SushiTableID { get; set; }
        public Guid SushiRacksTableID { get; set; }
        public Guid UsersTableID { get; set; }


        public void FindUserInfo()
        {
            Clear();
            User user = new();

            WriteLine($"Введи адрес электронной почты:");
            Validation.TryValidate(user, nameof(user.Mail));

            Clear();
            WriteLine($"Введи пароль:");
            Validation.TryValidate(user, nameof(user.Password));

            if (userBase?.GetItem(user) is User tempUser)
            {
                tempUser.GetInfo();
                return;
            }
            ReadKey();
        }

        //public void AllUsersInfo(UserBase userBase)
        //{
        //    Clear();
        //    userBase.GetAllItemsInfo(new User() { Name = "Администратор" });
        //    ReadKey();
        //}

        public void CreateSushiTable()
        {
            SushisDB sushis = new();
            sushis.CreateTable(Guid.NewGuid());
            SushiTableID = sushis.TableId;
        }

        public void CreateSushiRacksTable()
        {
            SushiRacksDB sushiRacks = new();
            sushiRacks.CreateTable(Guid.NewGuid());
            sushiRacks.TableId = SushiTableID;
            SushiRacksTableID = sushiRacks.TableId;
        }

        public void CreateUserBinTable(User user)
        {
            BinDB bin = new();
            bin.CreateTable(Guid.NewGuid());
            user.BinId = bin.TableId;
        }

        public void CreateUserArchiveTable(User user)
        {
            ArchiveDB archive = new();
            archive.CreateTable(Guid.NewGuid());
            user.ArchiveId = archive.TableId;
        }

        public void CreateUserOrderTable(User user)
        {
            OrderDB order = new();
            order.CreateTable(Guid.NewGuid());
            ArchiveDB archive = new() { TableId = user.ArchiveId };
            archive.CreateItem(order);
        }

        public void CreateUsersTable()
        {
            UsersDB users = new();
            users.CreateTable(Guid.NewGuid());
            UsersTableID = users.TableId;
        }

        public void AddUserToUsersTable()
        {
            UsersDB users = new() { TableId = UsersTableID };
            User user = Registration.RegistrateUser();
            users.CreateItem(user);
        }

        public void DeleteUser()
        {
            Clear();
            User user = new();

            WriteLine($"Введи адрес электронной почты:");
            Validation.TryValidate(user, nameof(user.Mail));

            Clear();
            WriteLine($"Введи пароль:");
            Validation.TryValidate(user, nameof(user.Password));

            if (userBase?.GetItem(user) is User tempUser)
            {
                WriteLine($"Пользователь {tempUser.Name} удален.");
                userBase.DeleteItem(tempUser);
            }
            ReadKey();
        }

        public void OpenOrder()
        {
            BinDB bin = new() { TableId = BinId };
            OrderDB order = new() { TableId = BinId };
            order.WriteAllItems(bin.ReadAllItems());
            bin.EmptyBin();
        }
    }
}
