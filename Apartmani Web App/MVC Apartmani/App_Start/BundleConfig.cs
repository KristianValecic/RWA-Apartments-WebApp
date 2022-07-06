using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace MVC_Apartmani.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundels) 
        {
            bundels.Add(new StyleBundle("~/Content").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/jquery.fancybox.css"
            ));

            bundels.Add(new ScriptBundle("~/Scripts").Include(
                "~/Scripts/jquery-3.6.0.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/jquery.fancybox.pack.js",
                "~/Scripts/jquery.fancybox.js",
                "~/Scripts/jquery.fancybox-buttons.js",
                "~/Scripts/jquery.fancybox-media.js",
                "~/Scripts/jquery.fancybox-thumbs.js"
            ));
        }
    }
}