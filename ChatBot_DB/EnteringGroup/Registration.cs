using static System.Console;

//namespace ChatBot_DB
//{
//    class Registration
//    {

//        public static ProtoUser RegistrateUser(UserBase userBase)
//        {
//            ProtoUser user = new();

//            user.ChangeName();
//            user.ChangePassword();
//            user.ChangeMail();
//            userBase.AddItem(user);

//            return user;
//        }

//        public static ProtoUser LogInUser(UserBase userBase)
//        {
//            Clear();
//            ProtoUser user = new();

//            user.ChangeMail();
//            user.ChangePassword();

//            ProtoUser = userBase.GetItem(user);

//            if (user != null) { return user; }

//            WriteLine("Данный пользователь не зарегистрирован.");
//            ReadKey();

//            return null;
//        }

//        public static UserAdmin LogInAdmin(UserBase userBase)
//        {
//            ProtoUser user = LogInUser(userBase);

//            return (user.Mail == "admin@mail.com") ? new() { Name = "Администратор" } : null;
//        }
//    }
//}
