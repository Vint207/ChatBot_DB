using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot_DB
{
    public class OrderDB : RacksDB
    {
    
        public DateTime OpenDate { get; set; }
        public DateTime CloseDate { get; set; }
        public bool Paid { get; set; }
        public bool Closed { get; set; }

        public void OpenOrder()
        {
            OpenDate = DateTime.Now;
        }

        public void PayOrder()
        {
            Paid = true;
        }

        public void CloseOrder()
        {
            CloseDate = DateTime.Now;
            Closed = true;
        }
    }
}
