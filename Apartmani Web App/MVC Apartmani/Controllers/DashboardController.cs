using Microsoft.AspNet.Identity.Owin;
using MVC_Apartmani.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVC_Apartmani.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private UserManager _authManager;

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

        // GET: Dashboard
        public async Task<ActionResult> Index()
        {
            var userId = User.Identity.Name;
            Models.User model = await AuthManager.FindByNameAsync(userId);
            return View(model);
        }
    }
}