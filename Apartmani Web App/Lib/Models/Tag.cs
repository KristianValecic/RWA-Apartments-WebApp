using Lib.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Models
{
    public class Tag
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string TypeOfTag { get; set; }
        public DateTime CreatedAt { get; set; }
        public int RecurrenceCounter { get; set; }
        public int Succsess { get; set; }

        public override string ToString() => Name;
        public override bool Equals(object obj) => obj is Tag other && ID == other.ID;
    }
}
