using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Models
{
    [Serializable]
    public class City
    {
        public City()
        {

        }

        public int ID { get; set; }
        public string Guid { get; set; }
        public string Name { get; set; }

        public override string ToString() => Name;
    }
}
