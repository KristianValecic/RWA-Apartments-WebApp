using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Models
{
    public class Reservation
    {
        private static char DELIM = ' ';

        public int ID { get; set; }
        public string UserName { get; set; }
        public string Details { get; set; } // mozda datetime
        public string Display
        {
            get => $"{UserName} {Details}";
        }

        public override string ToString() => $"{UserName}{DELIM}{Details}";

        public static Reservation Parse(string value)
        {
            string[] strings = value.Split(DELIM);
            return new Reservation { 
                UserName = strings[0],
                Details = strings[1]
            };
        }
    }
}
