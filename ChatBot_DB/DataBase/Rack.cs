using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot_DB
{
    public class Rack
    {

        public string Name { get; set; }
        public int Amount { get; set; } = 1;

        public void GetInfo() =>
            Console.WriteLine($"- {Name}. Количество {Amount} шт.");
    }
}
