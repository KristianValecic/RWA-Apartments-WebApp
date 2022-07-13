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
        private const string NAME = "Name";
        private const string PRICE = "Price";
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

        public ActionResult GetApartment(int id)
        {
            return Json(Repo.GetApartmentById(id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingleApartment(int id)
        {
            Lib.Models.Apartment apart = Repo.GetApartmentById(id);
            apart.Pictures = Repo.LoadImagesForAparment(id);
            return PartialView("_ApartmentContainer", apart);
        }

        public ActionResult FilterApartments(string city, string status, int? children, int? adults, int? rooms)
        {
            List<Lib.Models.Apartment> data = (List<Lib.Models.Apartment>)Repo.LoadAllApartments();


            _displayedApartments = data.Where(
                a => (string.IsNullOrEmpty(city)) ? true : a.City.ToLower().Equals(city.ToLower())
            ).Where(
                 a => (string.IsNullOrEmpty(status)) ? true : a.Status.ToLower().Equals(status.ToLower())
            ).Where(
                 a => (children == null) ? true : a.MaxChildren.Equals(children)
            ).Where(
                 a => (adults == null) ? true : a.MaxAdults.Equals(adults)
            ).Where(
                 a => (rooms == null) ? true : a.TotalRooms.Equals(rooms)
            ).OrderBy(a => a.ID);

            var jsonResult = Json(_displayedApartments, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult SortApartments(string valueTosort, string sortType)
        {
            if (_displayedApartments == null)
            {
                _displayedApartments = (List<Lib.Models.Apartment>)Repo.LoadAllApartments();
            }
            if (string.IsNullOrEmpty(valueTosort) && string.IsNullOrEmpty(sortType) ||
                string.IsNullOrEmpty(valueTosort) || string.IsNullOrEmpty(sortType))
            {
                _displayedApartments = _displayedApartments.OrderBy(a => a.ID);
            }
            else if (valueTosort == NAME)
            {
                SortDisplayedApartmentsByName(sortType);
            }
            else if (valueTosort == PRICE)
            {
                SortDisplayedApartmentsByPrice(sortType);
            }

            var jsonResult = Json(_displayedApartments, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        private void SortDisplayedApartmentsByName(string sortType)
        {
            if (sortType == ASC)
            {
                _displayedApartments = _displayedApartments.OrderBy(a => a.Name); //ToList().Sort((a, b) => a.Name.CompareTo(b.Name)); 
            }
            else if (sortType == DESC)
            {
                _displayedApartments = _displayedApartments.OrderByDescending(a => a.Name); //ToList().Sort((a, b) => -a.Name.First().CompareTo(b.Name.First()));
            }
        }

        private void SortDisplayedApartmentsByPrice(string sortType)
        {
            if (sortType == ASC)
            {
                _displayedApartments = _displayedApartments.OrderBy(a => a.Price); //ToList().Sort((a, b) => a.Price.CompareTo(b.Price));
            }
            else if (sortType == DESC)
            {
                _displayedApartments = _displayedApartments.OrderByDescending(a => a.Price); //ToList().Sort((a, b) => -a.Price.CompareTo(b.Price));
            }
        }
    }
}