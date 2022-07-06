using MVC_Apartmani.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MVC_Apartmani
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error()
        {
            string message = Server?.GetLastError().GetBaseException().Message;
            message = message.Replace("\r", "").Replace("\n", "");
            Response.Redirect("~/Error/Index?message=" + message);
        }
    }
}
