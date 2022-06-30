using Lib.Dal;
using Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.App_UserControls
{
    public partial class LoginControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PanelForma.Visible = true;
                PanelIspis.Visible = false;
            }

            if (Session["user"] != null)
            {
                Response.Redirect("Apartments.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //login iz baze
                //var Username = txtUsername.Text;
                //var Password = Cryptography.HashPassword(txtPassword.Text);
                //User user = ((DatabaseRepo)Application["database"]).AuthUser(Username, Password);

                var Username = txtUsername.Text;
                var Password = txtPassword.Text;

                User user = null;

                if (Username == "admin" && Password == "admin")
                {
                    user = new User
                    { 
                        UserName = Username,
                        Password = Password
                    };

                    Session["user"] = user;
                    Response.Redirect("Apartments.aspx");
                }

                if (user == null)
                {
                    PanelIspis.Visible = true;
                }
            }
        }
    }
}