using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Models
{
    public class Apartment
    {
        private Picture _representativePicture;
        private IList<Picture> _pictures;

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
        public int Stars { get; set; }
        public int totalPlacesCount
        {
            get => MaxChildren + MaxAdults;
        }
        public IList<Picture> Pictures
        {
            get => _pictures;
            set
            {
                _pictures = value;
                foreach (var item in _pictures)
                {
                    if (item.IsRepresentative)
                    {
                        _representativePicture = item;
                    }
                }
            }
        }
        public Picture RepresentativePicture { get => _representativePicture; }

        public override bool Equals(object obj)
        => obj is Apartment other && other.Name.Equals(Name) && other.Price.Equals(Price) && other.BeachDistance.Equals(BeachDistance);

        public override int GetHashCode()
        => GUID.GetHashCode();
    }
}
