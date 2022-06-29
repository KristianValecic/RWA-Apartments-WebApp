using Lib.Dal;
using Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Zadatak01
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            User u = (User)Session["user"];
            if (u != null)
            {
                pEmail.InnerText = u.Email;
                pCity.InnerText = u.City;
                pAddress.InnerText = u.Address;
            }
            bUsername.InnerText = u.UserName;
            base.OnPreRender(e);
        }

        //protected void btnLogout_Click(object sender, EventArgs e)
        //{
            
        //}
    }
}