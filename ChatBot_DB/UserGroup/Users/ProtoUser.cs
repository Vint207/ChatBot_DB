using System;
using System.ComponentModel.DataAnnotations;
using static System.Console;

namespace ChatBot_DB
{
    public abstract class ProtoUser
    {

        public bool Status { get; set; }
        public Guid UserID { get; set; }
        public Guid OrdersTableId { get; set; }
        public Guid BinTableId { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым. Введи новое имя:")]
        [RegularExpression(@"^[a-z\nA-Z\nа-я\nА-Я]{1,12}$", ErrorMessage = "Некорректный формат имени. Введи новое имя:")]
        public string Name { get; set; } = "Гость";

        [Required(ErrorMessage = "Поле не может быть пустым. Введи новый пароль:")]
        [RegularExpression(@"^[a-z\|A-Z\|0-9]{6,12}$", ErrorMessage = "Некорректный формат пароля. Введи новый пароль:")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым. Введи новый адрес почты:")]
        [RegularExpression(@"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
         @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$", ErrorMessage = "Недопустимый адрес электронной почты. Введи новый адрес почты:")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым. Введи сумму заново:")]
        [Range(1, 999999, ErrorMessage = "Сумма должна быть в диапазоне 1 - 999999 р. Введи сумму заново:")]
        public double Money { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым. Введи сумму заново:")]
        [Range(1, 9999, ErrorMessage = "Сумма должна быть в диапазоне 1 - 9999. Введи сумму заново:")]
        public double LastTransaction { get; set; }

        public void GetInfo()
        {
            //Clear();
            WriteLine();
            WriteLine($"Данные пользователя:");
            WriteLine($"Имя: {Name}\nПароль: {Password}\nБаланс: {Money} р\nПочта: {Mail}");
            //ReadKey();
        }
    }
}
