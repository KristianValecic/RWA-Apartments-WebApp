using Lib.Dal;
using Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp
{
    public partial class Apartments : System.Web.UI.Page
    {
        private IList<Apartment> _ListOfAllAparts;
        private IList<Apartment> _FilteredListOfAllAparts;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;

            _ListOfAllAparts = ((IRepo)Application["database"]).LoadApartments();
            _FilteredListOfAllAparts = _ListOfAllAparts;
            if (Session["user"] == null)
            {
                Response.Redirect("Default");
            }
            if (!IsPostBack)
            {
                LoadData();
                LoadDdl();
            }
        }

        private void LoadDdl()
        {
            IList<City> cities = ((IRepo)Application["database"]).LoadAllCities();
            ddlFilterCity.DataSource = cities;
            ddlFilterCity.DataBind();
            ddlFilterCity.Items.Insert(0, new ListItem(String.Empty, String.Empty));

            IList<Status> statuses = ((IRepo)Application["database"]).LoadAllStatuses();
            ddlFilterStatus.DataSource = statuses;
            ddlFilterStatus.DataValueField= nameof(Status.NameEng);
            ddlFilterStatus.DataTextField = nameof(Status.NameEng);
            ddlFilterStatus.DataBind();
            ddlFilterStatus.Items.Insert(0, new ListItem(String.Empty, String.Empty));
        }

        private void LoadData()
        {
            rptTags.DataSource = _ListOfAllAparts;
            rptTags.DataBind();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            var apartId = int.Parse(btn.CommandArgument);

            Session["Apartment"] = _ListOfAllAparts.FirstOrDefault(a => a.ID == apartId);
            Response.Redirect("EditApartment");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddApartment");

        }

        protected void ddlFilterCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            //_FilteredListOfAllAparts = FilteredList(_ListOfAllAparts, ((DropDownList)sender).SelectedValue);
            rptTags.DataSource = FilteredList(_ListOfAllAparts, ((DropDownList)sender).SelectedValue, ddlFilterStatus.SelectedValue);
            rptTags.DataBind();
        }

        protected void ddlFilterStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //_FilteredListOfAllAparts = FilteredList(_ListOfAllAparts, ((DropDownList)sender).SelectedValue);
            rptTags.DataSource = FilteredList(_ListOfAllAparts, ddlFilterCity.SelectedValue, ((DropDownList)sender).SelectedValue);
            rptTags.DataBind();
        }

        private IList<Apartment> FilteredList(IList<Apartment> listOfAllAparts, string selectedCity, string selectedStatus)
        {
            if (selectedCity == "" && selectedStatus == "")
            {
                return listOfAllAparts;
            }

            IList<Apartment> filteredList = new List<Apartment>();

            foreach (var apart in listOfAllAparts)
            {
                if (selectedCity != "" && selectedStatus != "")
                {
                    if (apart.City == selectedCity && apart.Status == selectedStatus)
                    {
                        filteredList.Add(apart);
                    }
                }
                else if (selectedCity != "")
                {
                    if (apart.City == selectedCity)
                    {
                        filteredList.Add(apart);
                    }
                }
                else if (selectedStatus != "")
                {
                    if (apart.Status == selectedStatus)
                    {
                        filteredList.Add(apart);
                    }
                }
            }

            return filteredList;
        }
    }
}