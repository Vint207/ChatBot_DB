using System;
using static System.Console;

namespace ChatBot_DB
{
    class Registration
    {

        public static User RegistrateUser(UserAdmin admin)
        {
            User user = new();
            user.UserID = Guid.NewGuid();
            user.UsersTableID = admin.UsersTableID;          
            user.SushiTableID = admin.SushiTableID;
            user.SushiRacksTableID = admin.SushiRacksTableID;

            ArchiveTable archive = new();            
            archive.CreateTable(Guid.NewGuid());
            user.ArchiveId = archive.TableId;       

            BinDB bin = new();
            bin.SushiTableId = admin.SushiTableID;
            bin.CreateTable(Guid.NewGuid());
            user.BinId = bin.TableId;
      
            user.CreateUser();

            return user;
        }

        public static User LogInUser(UserAdmin admin)
        {
            UsersTable users = new() { TableId = admin.UsersTableID };
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
    }
}
