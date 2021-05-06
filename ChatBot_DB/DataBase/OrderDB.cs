using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot_DB
{
    class OrderDB : RacksDB
    {
    
        public DateTime OpenDate { get; init; }
        public DateTime CloseDate { get; set; }
        public bool Paid { get; set; }
        public bool Closed { get; set; }
    }
}
