using System;

namespace ChatBot_DB
{
    public class Sushi
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public Sushi() {  }
        public Sushi(Guid id) { ID = id; }

        public void GetInfo()
        {
            Console.WriteLine($"- {Name}. Цена: {Price} р.");
        }
    }
}