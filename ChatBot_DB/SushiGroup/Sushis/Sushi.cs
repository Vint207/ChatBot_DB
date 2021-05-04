using System;

namespace Chat_Bot
{
    public sealed class Sushi
    {

        public string Name { get; set; }
        public double Price { get; set; }

        public Sushi(string name) { Name = name; }

        public Sushi(string name, double price) : this(name) { Price = price; }

        public void GetInfo(int amount) =>
            Console.WriteLine($"- {Name}. Цена: {Price} р. Осталось: {amount} шт");
    }
}