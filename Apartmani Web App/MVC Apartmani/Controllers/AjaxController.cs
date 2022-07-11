using MVC_Apartmani.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Apartmani.Controllers
{
    public class AjaxController : Controller
    {
        private const string ASC = "Asc";
        private const string DESC = "Desc";
        private static IEnumerable<Lib.Models.Apartment> _displayedApartments;
        //private SortedList<Lib.Models.Apartment, Lib.Models.Apartment> _displayedApartments;

        // GET: Ajax
        public ActionResult GetAutocompleteApartments(string term)
        {
            var data = Repo.LoadAllApartments();
            var find = data.Where(
                a => a.Name.ToLower().Contains(term.ToLower()) ||
                a.Owner.ToLower().Contains(term.ToLower())
            ).Select(a => new
            {
                label = a.Name,
                value = a.ID
            });
            return Json(find, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetApartments(int id)
        {
            return Json(Repo.GetApartmentById(id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingleApartment(int id)
        {
            Lib.Models.Apartment apart = Repo.GetApartmentById(id);
            apart.Pictures = Repo.loadImagesForAparment(id);
            return PartialView("_ApartmentContainer", apart);
        }

        public ActionResult FilterApartments(string city, string status, int? children, int? adults, int? rooms)
        {
            //List<Lib.Models.Apartment> data = (List<Lib.Models.Apartment>)Repo.LoadAllApartments();
            _displayedApartments = (IEnumerable<Lib.Models.Apartment>)Repo.LoadAllApartments();
            if (string.IsNullOrEmpty(city) && string.IsNullOrEmpty(status) && children == null && adults == null && rooms == null)
            {
                //data.Sort((a, b) => a.ID.CompareTo(b.ID));
                _displayedApartments.OrderBy(a => a.ID);
                return Json(_displayedApartments, JsonRequestBehavior.AllowGet);
            }

            _displayedApartments = _displayedApartments.Where(
                a => (string.IsNullOrEmpty(city)) ? true : a.City.ToLower().Equals(city.ToLower())
            ).Where(
                 a => (string.IsNullOrEmpty(status)) ? true : a.Status.ToLower().Equals(status.ToLower())
            ).Where(
                 a => (children == null) ? true : a.MaxChildren.Equals(children)
            ).Where(
                 a => (adults == null) ? true : a.MaxAdults.Equals(adults)
            ).Where(
                 a => (rooms == null) ? true : a.TotalRooms.Equals(rooms)
            );

            return Json(_displayedApartments, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SortApartments(string price, string name)
        {
            if (_displayedApartments == null)
            {
                _displayedApartments = (List<Lib.Models.Apartment>)Repo.LoadAllApartments();
            }
            if (string.IsNullOrEmpty(price) && string.IsNullOrEmpty(name))
            {
                _displayedApartments = _displayedApartments.OrderBy(a => a.ID);
            }
            else if (string.IsNullOrEmpty(price))
            {
                SortDisplayedApartmentsByName(name);
            }
            else if (string.IsNullOrEmpty(name))
            {
                SortDisplayedApartmentsByPrice(price);
            }

            return Json(_displayedApartments, JsonRequestBehavior.AllowGet);
        }

        private void SortDisplayedApartmentsByName(string name)
        {
            if (name == ASC)
            {
                _displayedApartments = _displayedApartments.OrderBy(a => a.Name).ThenBy(a => a.Name); //ToList().Sort((a, b) => a.Name.CompareTo(b.Name)); 
            }
            else if (name == DESC)
            {
                _displayedApartments = _displayedApartments.OrderByDescending(a => a.Name); //ToList().Sort((a, b) => -a.Name.First().CompareTo(b.Name.First()));
            }
        }

        private void SortDisplayedApartmentsByPrice(string price)
        {
            if (price == ASC)
            {
                _displayedApartments = _displayedApartments.OrderBy(a => a.Price); //ToList().Sort((a, b) => a.Price.CompareTo(b.Price));
            }
            else if (price == DESC)
            {
                _displayedApartments = _displayedApartments.OrderByDescending(a => a.Price); //ToList().Sort((a, b) => -a.Price.CompareTo(b.Price));
            }
        }
    }
}