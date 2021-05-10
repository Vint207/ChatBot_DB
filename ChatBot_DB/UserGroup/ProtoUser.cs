using System;
using System.ComponentModel.DataAnnotations;
using static System.Console;

namespace ChatBot_DB
{
    public abstract class ProtoUser
    {

        public bool Status { get; set; }
        public Guid UserID { get; set; }
        public Guid ArchiveId { get; set; }
        public Guid BinId { get; set; }
        public Guid LastOrderId { get; set; }
        public Guid SushiTableID { get; set; }
        public Guid SushiRacksTableID { get; set; }
        public Guid UsersTableID { get; set; }
       

        [Required(ErrorMessage = "Поле не может быть пустым. Введи новое имя:")]
        [RegularExpression(@"^[a-z\nA-Z\nа-я\nА-Я]{1,12}$", ErrorMessage = "Некорректный формат имени. Введи новое имя:")]
        public string Name { get; set; } = "Гость";

        [Required(ErrorMessage = "Поле не может быть пустым. Введи новый пароль:")]
        [RegularExpression(@"^[a-z\|A-Z\|0-9]{6,12}$", ErrorMessage = "Некорректный формат пароля. Введи новый пароль:")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым. Введи новый адрес почты:")] 
        [EmailAddress(ErrorMessage = "Недопустимый адрес электронной почты. Введи новый адрес почты:")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым. Введи сумму заново:")]
        [Range(1, 999999, ErrorMessage = "Сумма должна быть в диапазоне 1 - 999999 р. Введи сумму заново:")]
        public double Money { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым. Введи сумму заново:")]
        [Range(1, 9999, ErrorMessage = "Сумма должна быть в диапазоне 1 - 9999. Введи сумму заново:")]
        public double LastTransaction { get; set; }

        public void GetInfo()
        {
            Clear();
            WriteLine($"Данные пользователя:");
            WriteLine($"Имя: {Name}\nПароль: {Password}\nБаланс: {Money} р\nПочта: {Mail}");
            ReadKey();
        }
    }
}
