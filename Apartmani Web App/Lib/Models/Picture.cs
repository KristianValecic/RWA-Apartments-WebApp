using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Models
{
    public class Picture
    {
        public int ID { get; set; }
        public string Guid { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public bool IsRepresentitive { get; set; }

        public override string ToString() => Path;
    }
}
