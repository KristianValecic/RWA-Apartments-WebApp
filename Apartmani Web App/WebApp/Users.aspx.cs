using Lib.Dal;
using Lib.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp;

namespace WebApp
{
    public partial class Users : DefaultPage
    {
        private static int userId;
        //private User selectedUser;
        private IList<User> _ListOfAllUsers;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Default");
            }
          
            listTags.Visible = true;
            lblError.Visible = false;
            editUser.Visible = false;
            lblResult.Visible = false;

            _ListOfAllUsers = ((IRepo)Application["database"]).LoadUsers();
            if (!IsPostBack)
            {
                ShowUsers();
            }
        }

        private void ShowUsers()
        {
            //lbUsers.DataSource = _ListOfAllUsers;
            //lbUsers.DataValueField = "ID";
            //lbUsers.DataTextField = "Username";
            //lbUsers.DataBind();

            rptTags.DataSource = _ListOfAllUsers;
            rptTags.DataBind();
        }

        //protected void lbUsers_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //var userId = int.Parse(lbUsers.SelectedValue);

        //}

        protected void updateUser_Click(object sender, EventArgs e)
        {
            //var userId = int.Parse(lbUsers.SelectedValue);
            var selectedUser = _ListOfAllUsers.SingleOrDefault(u => u.ID == userId);

            selectedUser.UserName = txtUsername.Text;
            selectedUser.Email = txtEmail.Text;
            selectedUser.City = txtCity.Text;
            selectedUser.Address= txtAddress.Text;

            //selectedUser.Username = txtUsername.Text;

            //if (txtUsername.Text == "admin")
            //{

            //}

            try
            {
                ((IRepo)Application["database"]).UpdateUser(selectedUser);
                //User user = (User)Session["user"];
                //Session.Remove("user");           NE RADi
                //Session["user"] = user;
                Response.Redirect(Request.Url.LocalPath);
            }
            catch (SqlException ex)
            {
                lblResult.Text = "Error! Username already exists!";
                lblResult.Visible = true;
            }
            //catch (Exception)
            //{

            //}
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            userId = int.Parse(btn.CommandArgument);

            editUser.Visible = true;
            listTags.Visible = false;

            var selectedUser = _ListOfAllUsers.SingleOrDefault(u => u.ID == userId);

            if (selectedUser != null)
            {
                txtUsername.Text = selectedUser.UserName;
                txtEmail.Text = selectedUser.Email;
                txtAddress.Text = Lib.Models.User.GetStreetFromAdressLine(selectedUser.Address);
                txtCity.Text = selectedUser.City;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            editUser.Visible = false;
            listTags.Visible = true;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ((IRepo)Application["database"]).DeleteUser(userId);
            Response.Redirect(Request.Url.LocalPath);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registration");
        }
    }
}