using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot_DB
{
    public class ID
    {

        static int Id { get; set; } = 0;

        public ID() { Id += 1; }
    }
}
