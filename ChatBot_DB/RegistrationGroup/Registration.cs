using System;
using static System.Console;

namespace ChatBot_DB
{
    class Registration
    {

        public static User RegistrateUser(Guid userTableId)
        {
            User user = new();

            user.ChangeMail(userTableId);

            Clear();
            WriteLine($"Введи имя:");
            Validation.TryValidate(user, nameof(user.Name));        
         
            Clear();
            WriteLine($"Введи пароль:");
            Validation.TryValidate(user, nameof(user.Password));

            return user;
        }

        public static User LogInUser()
        {
            User user = new();

            Clear();
            WriteLine($"Введи адрес электронной почты:");
            Validation.TryValidate(user, nameof(user.Mail));

            Clear();
            WriteLine($"Введи пароль:");
            Validation.TryValidate(user, nameof(user.Password));

            //User = userBase.GetItem(user);

            if (user != null) { return user; }

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
