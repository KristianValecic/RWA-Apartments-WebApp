using Lib.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MVC_Apartmani.Models;
using MVC_Apartmani.Models.Auth;
using MVC_Apartmani.Models.CustomAttributes;
using MVC_Apartmani.Models.ViewModels;
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
            List<Apartment> model = Repo.LoadAllApartments().ToList();
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
            return View(Repo.GetApartmentById(ID));
        }
    }
} 