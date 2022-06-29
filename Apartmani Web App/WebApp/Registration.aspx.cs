using Lib.Dal;
using Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Zadatak01
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["user"] == null)
                {
                    //Response.Redirect("Dashboard");
                    Response.Redirect("Default");
                }
                    PanelForma.Visible = true;
                    PanelIspis.Visible = false;
                    AppendCity();

            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (IsPostBack && ViewState["user"] != null)
            {
                User u = (User)ViewState["user"];
                if (((IRepo)Application["database"]).AddUser(u) == 1)
                {
                    var method = Request.HttpMethod.ToLower();
                    Response.Redirect("Users");
                }
                else
                {
                    
                    lblUserAlreadyExists.Visible = true;
                }
                //Session["user"] = u;

                
            }
            base.OnPreRender(e);
        }

        private void AppendCity()
        {
            //ddlCity.Items.Add(new ListItem("Zagreb"));
            //ddlCity.Items.Add(new ListItem("Rijeka"));
            //ddlCity.Items.Add(new ListItem("Split"));
        }

        protected void Username_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (args.Value == "admin")
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void btnPosalji_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                User u = new User
                {
                    FName = txtFname.Text,
                    LName = txtLname.Text,
                    Email = txtEmail.Text,
                    City = txtCity.Text,
                    Address = txtAdress.Text,
                    Password = Cryptography.HashPassword(txtPassword.Text),
                    UserName = txtFname.Text + txtLname.Text, //this is here just to call the setter
                };
                ViewState["user"] = u;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Users");
        }
    }
}