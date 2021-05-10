using System;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace ChatBot_DB
{
    public class User : ProtoUser
    {

        public void ChangeName()
        {
            Clear();
            UsersTable users = new() { TableId = UsersTableID };

            WriteLine($"Введи имя (Используй только буквы):");

            Validation.TryValidate(this, nameof(Name));

            users.UpdateItem(this);
        }

        public void ChangePassword()
        {
            Clear();
            UsersTable users = new() { TableId = UsersTableID };

            WriteLine("Введи пароль аккаунта (6-12 букв латинского алфавита или цифр):");

            Validation.TryValidate(this, nameof(Password));

            users.UpdateItem(this);
        }

        public void ChangeMail()
        {
            Clear();
            UsersTable users = new() { TableId = UsersTableID };

            WriteLine("Введи адрес электронной почты:");

            while (true)
            {
                Validation.TryValidate(this, nameof(Mail));

                if (users.ReadItem(this) == null) { break; }

                WriteLine("Данный адрес электронной почты уже зарегистрирован. Попробуй другой:");
                ReadKey();
            }
            users.UpdateItem(this);
        }

        public User CreateUser()
        {
            Clear();
            UsersTable users = new() { TableId = UsersTableID };

            WriteLine("Введи адрес электронной почты:");

            while (true)
            {
                Validation.TryValidate(this, nameof(Mail));

                if (users.ReadItem(this) == null) { break; }

                Clear();
                WriteLine("Данный адрес электронной почты уже зарегистрирован. Попробуй другой:");
                ReadKey();
            }
            Clear();
            WriteLine($"Введи имя (Используй только буквы):");
            Validation.TryValidate(this, nameof(Name));

            Clear();
            WriteLine("Введи пароль аккаунта (6-12 букв латинского алфавита или цифр):");
            Validation.TryValidate(this, nameof(Password));

            return this;
        }

        public void PutMoney()
        {
            Clear();
            UsersTable users = new() { TableId = UsersTableID };

            WriteLine($"На счету {Name} {Money} р");

            WriteLine($"Введи сумму для перевода:");

            Validation.TryValidate(this, nameof(LastTransaction));

            Money += LastTransaction;

            users.UpdateItem(this);

            Clear();
            WriteLine($"Баланс {Name} составляет {Money} р");
            ReadKey();
        }    
        
        public void CreateUserBinTable()
        {
            BinDB bin = new();
            bin.CreateTable(Guid.NewGuid());
            BinId = bin.TableId;
        }

        public void CreateUserArchiveTable()
        {
            ArchiveTable archive = new();
            archive.CreateTable(Guid.NewGuid());
            ArchiveId = archive.TableId;
        }
    }
}
