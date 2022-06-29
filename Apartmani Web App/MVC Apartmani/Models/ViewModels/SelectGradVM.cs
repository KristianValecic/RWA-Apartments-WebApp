using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Apartmani.Models.ViewModels
{
    public class SelectGradVM
    {
        public IEnumerable<Drzava> Drzave { get; set; }
        public IEnumerable<Grad> Gradovi { get; set; }
    }
}