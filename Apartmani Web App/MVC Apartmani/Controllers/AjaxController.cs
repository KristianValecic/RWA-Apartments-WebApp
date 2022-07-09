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
        public ActionResult getAutocompleteApartments(string term)
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

        public ActionResult getApartments(int id)
        {
            return Json(Repo.GetApartmentById(id), JsonRequestBehavior.AllowGet);
        }
        public ActionResult getSingleApartment(int id)
        {
            Lib.Models.Apartment apart = Repo.GetApartmentById(id);
            apart.Pictures = Repo.loadImagesForAparment(id);
            return PartialView("_ApartmentContainer", apart);
        }
    }
}