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
    public partial class Tags : DefaultPage
    {
        private IList<Tag> _LisOfAllTags;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Default");
            }


            addTags.Visible = false;
            lblError.Visible = false;
            //panelConfirm.Visible = false;

            _LisOfAllTags = ((DatabaseRepo)Application["database"]).LoadTags();
            if (!IsPostBack)
            {
                LoadDdlData();
                LoadData();
            }
        }

        private void LoadDdlData()
        {
            //Array types = Enum.GetValues(typeof(TagType));
            IList<string> types = ((IRepo)Application["database"]).LoadAllTypesOfTags();

            foreach (var type in types)
            {
                ddlTagType.Items.Add(new ListItem(type.ToString()));
            }
        }

        private void LoadData()
        {
            rptTags.DataSource = _LisOfAllTags;
            rptTags.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //if (txtName.Text == String.Empty)
            //{

            //}
            try
            {
                ((IRepo)Application["database"]).AddTag(new Tag
                {
                    Name = txtName.Text,
                    TypeOfTag = ddlTagType.SelectedValue
                });
                Response.Redirect(Request.Url.LocalPath);
            }
            catch (SqlException)
            {
                lblError.Text = "Error!";
                lblError.Visible = true;
                //Response.Redirect(Request.Url.LocalPath);

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            addTags.Visible = true;
            listTags.Visible = false;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
                LinkButton btn = (LinkButton)sender;
                int tagID = int.Parse(btn.CommandArgument);

                ((IRepo)Application["database"]).DeleteTag(tagID);
                Response.Redirect(Request.Url.LocalPath);  
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            addTags.Visible = false;
            listTags.Visible = true;
        }
    }
}