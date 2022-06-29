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
    public partial class AddApartment : System.Web.UI.Page
    {
        private string Reserved = "Reserved";
        private string Vacant = "Vacant";
        private Apartment tempApartment;
        private IList<City> cities;
        private static IList<Tag> tempTags = new List<Tag>();
        private static IList<Reservation> tempReservations = new List<Reservation>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Default");
            }

            if (!IsPostBack)
            {
                LoadDdl();
                LoadListBox();
            }
            //else
            //{
            //    SaveInsertedData();
            //    LoadData();
            //}

        }

        //private void LoadData()
        //{
        //    txtName.Text = txtNameHidden.Value;
        //    txtOwner.Text = txtOwnerHidden.Value;
        //    txtAddress.Text = txtAdressHidden.Value;
        //    txtMaxChildren.Text = txtMaxChildrenHidden.Value;
        //    txtMaxAdults.Text = txtMaxAdultsHidden.Value;
        //    //ddlStatuses.SelectedValue = txtStatusHidden.Value;
        //    txtPrice.Text = txtPriceHidden.Value;
        //    txtRooms.Text = txtRoomsHidden.Value;
        //    txtBeachDistance.Text = txtBeachDistanceHidden.Value;

        //    //ddlCity.SelectedValue = txtCityHidden.Value;
        //}

        private void LoadListBox()
        {
            lsTags.DataSource = tempTags;
            lsTags.DataValueField = nameof(Tag.ID);
            lsTags.DataTextField = nameof(Tag.Name);
            lsTags.DataBind();

            lsReservations.DataSource = tempReservations;
            lsReservations.DataValueField = nameof(Reservation.ID);
            lsReservations.DataTextField = nameof(Reservation.Display);
            lsReservations.DataBind();
        }

        private void LoadDdl()
        {
            IList<City> cities = ((IRepo)Application["database"]).LoadAllCities();
            ddlCity.DataSource = cities;
            ddlCity.DataBind();

            IList<Status> statuses = ((IRepo)Application["database"]).LoadAllStatuses();
            ddlStatuses.DataSource = statuses;
            ddlStatuses.DataValueField = nameof(Status.Name);
            ddlStatuses.DataTextField = nameof(Status.NameEng);
            //ddlStatuses.SelectedValue = Vacant;
            ddlStatuses.DataBind();

            ddlAllTags.DataSource = ((IRepo)Application["database"]).LoadTags();
            ddlAllTags.DataValueField = nameof(Tag.ID);
            ddlAllTags.DataTextField = nameof(Tag.Name);
            ddlAllTags.DataBind();
        }

        protected void btnAddApart_Click(object sender, EventArgs e)
        {
            tempApartment = new Apartment
            {
                Name = txtName.Text,
                Owner = txtOwner.Text,
                City = ddlCity.SelectedValue,
                Address = txtAddress.Text,
                MaxChildren = int.Parse(txtMaxChildren.Text),
                MaxAdults = int.Parse(txtMaxAdults.Text),
                Status = ddlStatuses.SelectedValue,
                Price = decimal.Parse(txtPrice.Text),
                TotalRooms = int.Parse(txtRooms.Text),
                BeachDistance = int.Parse(txtBeachDistance.Text)
            };

            ((IRepo)Application["database"]).AddApartment(tempApartment);

            //((IRepo)Application["database"]).AddApartment(tempApartment);

            Apartment tempApart = (Apartment)((IRepo)Application["database"]).GetApartment(tempApartment.Name, tempApartment.Owner, tempApartment.Address);

            foreach (ListItem tag in lsTags.Items)
            {
                int succeeded = ((IRepo)Application["database"]).AddTagToApartment(int.Parse(tag.Value), tempApart.ID);
            }
            
            foreach (ListItem reservation in lsReservations.Items)
            {
                Reservation tempRes = Reservation.Parse(reservation.Text);
                int succeeded = ((IRepo)Application["database"]).AddReservationToApartment(tempRes.UserName, tempRes.Details, tempApart.ID);
                ((IRepo)Application["database"]).SetReserved(tempApart.ID);
            }

            Response.Redirect("Apartments");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Apartments");
        }

        protected void btnAddTag_Click(object sender, EventArgs e)
        {
            if (tempTags.Count != 0 &&
                tempTags.FirstOrDefault(t => t.ID == int.Parse(ddlAllTags.SelectedValue)) != null)
            {
                if (tempTags.FirstOrDefault(t => t.ID == int.Parse(ddlAllTags.SelectedValue)).Equals((Tag)((IRepo)Application["database"]).GetTagById(ddlAllTags.SelectedValue)))
                {
                    lblTagValidation.Visible = true;
                    return;
                }
            }

            tempTags.Add((Tag)((IRepo)Application["database"]).GetTagById(ddlAllTags.SelectedValue));

            Response.Redirect(Request.Url.LocalPath);
        }

        private void SaveInsertedData()
        {
            //txtNameHidden.Value = txtName.Text;
            //txtOwnerHidden.Value = txtOwner.Text;
            //txtCityHidden.Value = ddlCity.SelectedValue;
            //txtAdressHidden.Value = txtAddress.Text;
            //txtMaxChildrenHidden.Value = txtMaxChildren.Text;
            //txtMaxAdultsHidden.Value = txtMaxAdults.Text;
            //txtStatusHidden.Value = ddlStatuses.SelectedValue;
            //txtPriceHidden.Value = txtPrice.Text;
            //txtRoomsHidden.Value = txtRooms.Text;
            //txtBeachDistanceHidden.Value = txtBeachDistance.Text;

            //txtNameHidden.Value = txtName.Text;
            //txtOwnerHidden.Value = txtOwner.Text;
            //txtCityHidden.Value = ddlCity.SelectedValue;
            //txtAdressHidden.Value = txtAddress.Text;
            //txtMaxChildrenHidden.Value = txtMaxChildren.Text;
            //txtMaxAdultsHidden.Value = txtMaxAdults.Text;
            //txtStatusHidden.Value = ddlStatuses.SelectedValue;
            //txtPriceHidden.Value = txtPrice.Text;
            //txtRoomsHidden.Value = txtRooms.Text;
            //txtBeachDistanceHidden.Value = txtBeachDistance.Text;
        }

        protected void btnDeleteTag_Click(object sender, EventArgs e)
        {
            tempTags.Remove((Tag)((IRepo)Application["database"]).GetTagById(lsTags.SelectedValue));
            Response.Redirect(Request.Url.LocalPath);
        }

        protected void lsTags_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnDeleteTag.Visible = true;
        }

        protected void btnDeleteReservation_Click(object sender, EventArgs e)
        {
            //tempReservations.Remove();
            Response.Redirect(Request.Url.LocalPath);
        }

        protected void btnAddReservation_Click(object sender, EventArgs e)
        {

            tempReservations.Add(new Reservation
            {
                UserName = txtReservationName.Text,
                Details = txtDate.Text
            });

            Response.Redirect(Request.Url.LocalPath);
        }

        protected void lsReservations_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnDeleteReservation.Visible = true;
        }
    }
}