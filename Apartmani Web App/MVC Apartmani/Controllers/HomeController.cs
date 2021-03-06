using Lib.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MVC_Apartmani.Models;
using MVC_Apartmani.Models.Auth;
using MVC_Apartmani.Models.CustomAttributes;
using MVC_Apartmani.Models.ViewModels;
using Recaptcha.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVC_Apartmani.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private UserManager _authManager;
        private SignInManager _singInManager;

        public SignInManager SignInManager
        {
            get
            {
                return _singInManager ?? HttpContext.GetOwinContext().Get<SignInManager>();
            }
            private set
            {
                _singInManager = value;
            }
        }

        public UserManager AuthManager 
        {
            get
            {
                return _authManager ?? HttpContext.GetOwinContext().GetUserManager<UserManager>();
            }
            private set
            {
                _authManager = value;
            }
        }

        // GET: Home
        [HttpGet]
        [AllowAnonymous]
        //[IsAuthorized]
        public ActionResult Index()
        {
            //List<Apartment> model = Repo.LoadAllApartments().ToList();

            if (HttpContext.Request.Cookies["search"] != null && User.Identity.IsAuthenticated &&
                HttpContext.Request.Cookies["search"].Values["User"] == User.Identity.GetUserId())
            {
                HttpCookie cookie = Request.Cookies["search"];

                TempData["City"] = cookie.Values["City"];
                TempData["Status"] = cookie.Values["Status"];
                TempData["Children"] = cookie.Values["Children"];
                TempData["Adults"] = cookie.Values["Adults"];
                TempData["Rooms"] = cookie.Values["Rooms"];
            }

            IndexApartmentViewModel model = new IndexApartmentViewModel
            {
                Apartments = Repo.LoadAllApartments().ToList(),
                Cities = Repo.LoadAllCities(),
                Statuses = Repo.LoadAllStatuses(),
            };

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        [IsAuthorized]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await AuthManager.FindAsync(model.UserName, model.Password);
            if (user != null)
            {
                await SignInManager.SignInAsync(user, true, model.RememberMe);
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ModelState.AddModelError("", "User doesnt exist");
                return View(model);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [IsAuthorized]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new Models.User
            {
                UserName = model.UserName,
                Email = model.Email,
                City = model.City,
                Address = model.Address,
                Password = model.Password,
            };

            if (Repo.AddUser(user) == 0)
            {
                ViewBag.CreateError = true;

            }

            if (user != null)
            {
                await SignInManager.SignInAsync(user, true, false);
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ModelState.AddModelError("", "There was an error with the registration");
                return View(model);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [AllowAnonymous]
        //[IsAuthorized]
        public ActionResult SelectApartment(int ID)
        {
            Apartment apart = Repo.GetApartmentById(ID);

            if (User.Identity.IsAuthenticated)
            {
                ViewBag.User = Repo.GetUserById(User.Identity.GetUserId());
                apart.Stars = Repo.GetApartmentRatingFromUser(ID, int.Parse(User.Identity.GetUserId()));
            }
            
            apart.Pictures = Repo.LoadImagesForAparment(ID);
            ReservationViewModel model = new ReservationViewModel
            {
                Apart = apart
            };
            return View(model);
        }



        [HttpPost]
        [AllowAnonymous]
        public ActionResult SelectApartment(ReservationViewModel model)
        {
            Apartment apart = Repo.GetApartmentById(model.Apart.ID);
            apart.Pictures = Repo.LoadImagesForAparment(model.Apart.ID);
            model.Apart = apart;

            if (!User.Identity.IsAuthenticated)
            {
                var recaptchaHelper = this.GetRecaptchaVerificationHelper();
                if (String.IsNullOrEmpty(recaptchaHelper.Response))
                {
                    ModelState.AddModelError(
                    "",
                    "Captcha answer cannot be empty.");
                    return View(model);
                }

                var recaptchaResult = recaptchaHelper.VerifyRecaptchaResponse();
                if (!recaptchaResult.Success)
                {
                    ModelState.AddModelError(
                    "",
                    "Incorrect captcha answer.");
                    return View(model);
                } 
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (Repo.AddReservationToApartment(model.Name, model.Email, model.Address, model.Details, model.Apart.ID) == 0)
            {
                ViewBag.IsReserved = false;
                return View(model);
            }
            else
            {
                ViewBag.IsReserved = true;
            }


            return View(model);
        }
    }
} 