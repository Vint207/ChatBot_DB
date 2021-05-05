using System;
using static System.Console;

namespace ChatBot_DB
{
    public sealed class UserAdmin : UserMiddle
    {

        public void FindUserInfo(UserBase userBase)
        {
            Clear();
            UserMiddle user = new();

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

        public void AllUsersInfo(UserBase userBase)
        {
            Clear();
            userBase.GetAllItemsInfo(new UserMiddle() { Name = "Администратор" });
            ReadKey();
        }

        public void DeleteUser(UserBase userBase)
        {
            Clear();
            UserMiddle user = new();

            WriteLine($"Введи адрес электронной почты:");
            Validation.TryValidate(user, nameof(user.Mail));

            Clear();
            WriteLine($"Введи пароль:");
            Validation.TryValidate(user, nameof(user.Password));

            if (userBase?.GetItem(user) is UserMiddle tempUser)
            {
                WriteLine($"Пользователь {tempUser.Name} удален.");
                userBase.DeleteItem(tempUser);
            }
            ReadKey();
        }
    }
}
