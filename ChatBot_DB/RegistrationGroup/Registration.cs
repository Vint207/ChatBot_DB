using System;
using static System.Console;

namespace ChatBot_DB
{
    class Registration
    {

        public static User RegistrateUser(Guid userTableId, Guid sushiTableId, Guid sushiRacksTableId)
        {
            User user = new();
            user.UserID = Guid.NewGuid();
            user.UsersTableID = userTableId;          
            user.SushiTableID = sushiTableId;
            user.SushiRacksTableID = sushiRacksTableId;

            ArchiveDB archive = new();            
            archive.CreateTable(Guid.NewGuid());
            user.ArchiveId = archive.TableId;       

            BinDB bin = new();
            bin.SushiTableId = sushiTableId;
            bin.CreateTable(Guid.NewGuid());
            user.BinId = bin.TableId;
      
            user.CreateUser();

            return user;
        }

        public static User LogInUser(Guid userTableId)
        {
            UsersDB users = new() { TableId = userTableId };
            User user = new();

            Clear();
            WriteLine($"Введи адрес электронной почты:");
            Validation.TryValidate(user, nameof(user.Mail));

            Clear();
            WriteLine($"Введи пароль:");
            Validation.TryValidate(user, nameof(user.Password));

            User tempUser = users.ReadItem(user);

            if (tempUser != null) {  return tempUser; }

            WriteLine("Данный пользователь не зарегистрирован.");
            ReadKey();

            return null;
        }

        //public static UserAdmin LogInAdmin(UserBase userBase)
        //{
        //    ProtoUser user = LogInUser(userBase);

        //    return (user.Mail == "admin@mail.com") ? new() { Name = "Администратор" } : null;
        //}
    }
}
