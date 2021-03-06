using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp
{
    public partial class Settings : DefaultPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PreselectedDDLAfterRefresh();
        }

        private void PreselectedDDLAfterRefresh()
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Default");
            }
            
            if (!IsPostBack)
            {
                ddlTheme.SelectedIndex = DDLThemeIndex;
            }
        }

        protected void ddlTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTheme.SelectedValue != "0")
            {
                HttpCookie cookieTheme = new HttpCookie("theme");
                cookieTheme.Expires.AddDays(10);

                cookieTheme["theme"] = ddlTheme.SelectedValue;
                cookieTheme["index"] = ((DropDownList)sender).SelectedIndex.ToString();

                Response.Cookies.Add(cookieTheme);
                Response.Redirect(Request.Url.LocalPath);
            }
        }

        //protected void ddlLang_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlLang.SelectedValue != "0")
        //    {
        //        HttpCookie cookieLang = new HttpCookie("lang");
        //        cookieLang.Expires.AddDays(10);

        //        cookieLang["lang"] = ddlLang.SelectedValue;
        //        cookieLang["index"] = ((DropDownList)sender).SelectedIndex.ToString();

        //        Response.Cookies.Add(cookieLang);
        //        Response.Redirect(Request.Url.LocalPath);
        //    }
        //}
    }
}