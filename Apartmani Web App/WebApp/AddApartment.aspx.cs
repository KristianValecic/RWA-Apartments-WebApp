using Lib.Dal;
using Lib.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.App_UserControls;

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
        private static IList<Picture> addedPicutres = new List<Picture>();
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
                LoadImgs();
                if (Session["Apartment"] != null)
                {
                    LoadData();
                }
            }
            else
            {
                SaveApartToSesion();
            }
        }

        private void LoadImgs()
        {
            //foreach (Picture img in addedPicutres)
            //{
            //    ImageControl control = new ImageControl();
            //}
            rptrImages.DataSource = addedPicutres;
            rptrImages.Controls.Add(new ImageControl());
            rptrImages.DataBind();
        }

        private void LoadData()
        {
            txtName.Text = ((Apartment)Session["Apartment"]).Name;
            txtOwner.Text = ((Apartment)Session["Apartment"]).Owner;
            txtAddress.Text = ((Apartment)Session["Apartment"]).Address;
            txtMaxChildren.Text = ((Apartment)Session["Apartment"]).MaxChildren.ToString();
            txtMaxAdults.Text = ((Apartment)Session["Apartment"]).MaxAdults.ToString();
            txtPrice.Text = ((Apartment)Session["Apartment"]).Price.ToString();
            txtRooms.Text = ((Apartment)Session["Apartment"]).TotalRooms.ToString();
            txtBeachDistance.Text = ((Apartment)Session["Apartment"]).BeachDistance.ToString();
            ddlStatuses.SelectedValue = ((Apartment)Session["Apartment"]).Status;
            //ddlStatuses.SelectedValue = txtStatusHidden.Value;

            //ddlCity.SelectedValue = txtCityHidden.Value;
        }

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
            ddlStatuses.DataValueField = nameof(Status.NameEng);
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

            Apartment tempApart = (Apartment)((IRepo)Application["database"]).GetApartment(tempApartment.Name, tempApartment.Owner, tempApartment.Address);

            try
            {
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

                foreach (Picture img in addedPicutres)
                {
                    ((IRepo)Application["database"]).AddImageForAparment(img, tempApart.ID);
                }
            }
            catch (Exception)
            {
                //handle exception
            }

            Session["Apartment"] = null;
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

        private void SaveApartToSesion()
        {
            Session["Apartment"] = new Apartment
            {
                Name = txtName.Text,
                Owner = txtOwner.Text,
                City = ddlCity.SelectedValue,
                Address = txtAddress.Text,
                MaxChildren = String.IsNullOrEmpty(txtMaxChildren.Text) ? 0 : int.Parse(txtMaxChildren.Text),
                MaxAdults = String.IsNullOrEmpty(txtMaxAdults.Text) ? 0 : int.Parse(txtMaxAdults.Text),
                Status = ddlStatuses.SelectedValue,
                Price = String.IsNullOrEmpty(txtPrice.Text) ? 0 : decimal.Parse(txtPrice.Text),
                TotalRooms = String.IsNullOrEmpty(txtRooms.Text) ? 0 : int.Parse(txtRooms.Text),
                BeachDistance = String.IsNullOrEmpty(txtBeachDistance.Text) ? 0 : int.Parse(txtBeachDistance.Text)
            };
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

        protected void btnAddImg_Click(object sender, EventArgs e)
        {
            if (FileUpload.HasFile)
            {
                addedPicutres.Add(new Picture
                {
                    Name = txtImgName.Text,
                    IsRepresentitive = cbIsRepresentative.Checked,
                    Base64 = GetBase64(FileUpload.FileName)
                });

                Response.Redirect(Request.Url.LocalPath);
            }
            else
            {
                lblSelectedFileError.Visible = false;
                lblSelectedFileMessage.Visible = true;
            }
        }
        private string GetBase64(string imgName)
        {
            string imgPath = Server.MapPath("~/uploads/");

            if (!Directory.Exists(imgPath))
            {
                Directory.CreateDirectory(imgPath);
            }

            string imgFilePath = imgPath + imgName;
            FileUpload.SaveAs(imgFilePath);

            byte[] imgArray = File.ReadAllBytes(imgFilePath);
            return Convert.ToBase64String(imgArray);
        }
    }
}