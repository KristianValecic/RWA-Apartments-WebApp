using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Models
{
    public class Apartment
    {
        public int ID { get; set; }
        public string GUID { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }   //User
        public string City { get; set; }    //city
        public string Address { get; set; }    
        public decimal Price { get; set; }
        public string Status { get; set; }
        public int TotalRooms { get; set; }
        public int BeachDistance { get; set; }
        public int MaxAdults { get; set; }
        public int MaxChildren { get; set; }
        public int totalPlacesCount {
            get => MaxChildren + MaxAdults;
        }
        public IList<Picture> Pictures { get; set; }
    }
}
