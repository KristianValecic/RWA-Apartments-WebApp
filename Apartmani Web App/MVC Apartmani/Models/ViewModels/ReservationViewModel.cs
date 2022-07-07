using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Apartmani.Models.ViewModels
{
    public class ReservationViewModel
    {
        private string _from;
        private string _to;

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Krivi Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Addres is required")]
        public string Address { get; set; }
        public int Children { get; set; }
        [Required(ErrorMessage = "From is required")]
        public string From 
        { 
            get => _from; 
            set => _from = value; 
        }
        [Required(ErrorMessage = "To is required")]
        public string To
        {
            get => _from;
            set => _to = value; 
        }
        public string Details { get => $"{_from} - {_to}"; }
        public Lib.Models.Apartment Apart { get; set; }
        //public int Apart_ID { get; set; }
    }
}