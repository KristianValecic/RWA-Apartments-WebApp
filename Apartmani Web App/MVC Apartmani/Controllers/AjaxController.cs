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

        public ActionResult SortApartments(string city, string status)
        {
            List<Lib.Models.Apartment> data = (List<Lib.Models.Apartment>)Repo.LoadAllApartments();
            if (string.IsNullOrEmpty(city) && string.IsNullOrEmpty(status))
            {
                data.Sort((a, b) => a.ID.CompareTo(b.ID));
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else if (string.IsNullOrEmpty(city))
            {
                return Json(
                data.Where(a => a.Status.ToLower().Contains(status.ToLower())),
                JsonRequestBehavior.AllowGet);
            }
            else if (string.IsNullOrEmpty(status))
            {
                return Json(
                data.Where(a => a.City.ToLower().Contains(city.ToLower())),
                JsonRequestBehavior.AllowGet);
            }

            var find = data.Where(
                a => a.City.ToLower().Contains(city.ToLower())
                && a.Status.ToLower().Contains(status.ToLower())
            );

            return Json(find, JsonRequestBehavior.AllowGet);
        }
    }
}