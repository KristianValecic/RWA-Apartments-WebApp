using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Apartmani.Models.ViewModels
{
    public class IndexApartmentViewModel
    {
        public IList<Lib.Models.Apartment> Apartments { get; set; }
        public IList<Lib.Models.Status> Statuses { get; set; }
        public IList<Lib.Models.City> Cities { get; set; }

    }
}