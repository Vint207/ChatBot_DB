using System;

namespace ChatBot_DB
{
    public class SushiRack
    {

        public string Name { get; set; }
        public int Amount { get; set; }

        public void GetInfo(Sushi sushi)
        {
            Console.WriteLine($"- {Name}. Количество {Amount} шт.");
        }
    }
}
