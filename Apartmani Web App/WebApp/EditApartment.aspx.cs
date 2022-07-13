using Lib.Dal;
using Lib.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp
{
    public partial class EditApartment : System.Web.UI.Page
    {
        private string Vacant = "Vacant";
        private string Reserved = "Reserved";
        private Apartment apartment;
        private Tag selectedTag;
        private IList<Tag> listofAllTags;

        protected void Page_Load(object sender, EventArgs e)
        {
            apartment = (Apartment)Session["Apartment"];
            if (apartment == null)
            {
                Response.Redirect("Apartments");
            }
            if (!IsPostBack)
            {
                LoadData();
                LoadDdl();
                LoadListBox();
                LoadRepeater();
            }
        }

        private void LoadRepeater()
        {
            rptrImages.DataSource = ((IRepo)Application["database"]).LoadImagesForAparment(((Apartment)Session["apartment"]).ID);
            rptrImages.DataBind();
            //if (rptrImages.Controls.Count == 0)
            //{
            //    btnChangeImg.Text = "Add image";
            //}
        }

        private void LoadListBox()
        {
            listofAllTags = ((IRepo)Application["database"]).LoadTagsForApartment(apartment.ID);
            lsTags.DataSource = listofAllTags;
            lsTags.DataValueField = nameof(Tag.ID);
            lsTags.DataTextField = nameof(Tag.Name);
            lsTags.DataBind();

            lsReservations.DataSource = ((IRepo)Application["database"]).LoadAllReservationsForApartment(apartment.ID);
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
            ddlStatuses.DataBind();

            ddlAllTags.DataSource = ((IRepo)Application["database"]).LoadTags();
            ddlAllTags.DataValueField = nameof(Tag.ID);
            ddlAllTags.DataTextField = nameof(Tag.Name);
            ddlAllTags.DataBind();
        }

        private void LoadData()
        {
            if (apartment != null)
            {
                txtName.Text = apartment.Name;
                ddlCity.SelectedValue = apartment.City;
                txtAddress.Text = apartment.Address;
                txtMaxChildren.Text = apartment.MaxChildren.ToString();
                txtMaxAdults.Text = apartment.MaxAdults.ToString();
                ddlStatuses.SelectedValue = apartment.Status;
                txtPrice.Text = apartment.Price.ToString();
                txtRooms.Text = apartment.TotalRooms.ToString();
                txtBeachDistance.Text = apartment.BeachDistance.ToString();

                lblOwnerName.Text = apartment.Owner;
                headerName.InnerText = apartment.Name;

                //apartment.PicturePaths = ((IRepo)Application["database"]).loadImagesForAparment(apartment.ID);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Apartments");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (apartment != null)
            {
                apartment.Name = txtName.Text;
                apartment.City = ddlCity.SelectedValue;
                apartment.Address = txtAddress.Text;
                apartment.MaxChildren = int.Parse(txtMaxChildren.Text);
                apartment.MaxAdults = int.Parse(txtMaxAdults.Text);
                apartment.Status = ddlStatuses.SelectedValue;
                apartment.Price = decimal.Parse(txtPrice.Text);
                apartment.TotalRooms  = int.Parse(txtRooms.Text);
                apartment.BeachDistance = int.Parse(txtBeachDistance.Text);

                //lblOwnerName.Text = apartment.Owner;
                //headerName.InnerText = apartment.Name;

                //apartment.Pictures = ((IRepo)Application["database"]).loadImagesForAparment(apartment.ID);
            }

            ((IRepo)Application["database"]).UpdateApartment(apartment);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ((IRepo)Application["database"]).DeleteApartment(apartment.ID);
            Response.Redirect("Apartments");
        }

        protected void lsTags_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnDeleteTag.Visible = true;
        }

        protected void btnDeleteTag_Click(object sender, EventArgs e)
        {
            ((IRepo)Application["database"]).RemoveTagFromApartment(int.Parse(lsTags.SelectedValue), ((Apartment)Session["apartment"]).ID);
            Response.Redirect(Request.Url.LocalPath);
        }

        protected void btnAddTag_Click(object sender, EventArgs e)
        {

            if (((IRepo)Application["database"]).AddTagToApartment(int.Parse(ddlAllTags.SelectedValue), ((Apartment)Session["apartment"]).ID) == 0) 
            {
                lblTagValidation.Visible = true;
            }
            else
            {
                Response.Redirect(Request.Url.LocalPath);
            }
        }

        protected void ddlStatuses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlStatuses.SelectedValue == Vacant)
            {
                //izbrisi sve iz rezervaztions
            }
        }

        protected void btnDeleteReservation_Click(object sender, EventArgs e)
        {
            ((IRepo)Application["database"]).RemoveReservationFromApartment(int.Parse(lsReservations.SelectedValue), ((Apartment)Session["apartment"]).ID);
            Response.Redirect(Request.Url.LocalPath);
        }

        protected void btnAddReservation_Click(object sender, EventArgs e)
        {
            if (lsReservations.Controls.Count == 0)
            {
                ((IRepo)Application["database"]).SetReserved(((Apartment)Session["apartment"]).ID);
            }
            if (((IRepo)Application["database"]).AddReservationToApartment(txtReservationName.Text, "", "", txtDate.Text, ((Apartment)Session["apartment"]).ID) == 0)
            {
                lblReservaionValidation.Visible = true;
            }
            else
            {
                //ddlStatuses.SelectedValue = Reserved;
                apartment.Status = Reserved;
                LoadData();
                Response.Redirect(Request.Url.LocalPath);
            }
        }

        protected void lsReservations_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnDeleteReservation.Visible = true;
        }

        protected void btnChangeImg_Click(object sender, EventArgs e)
        {
            UploadImage();
        }

        private void UploadImage()
        {
            if (FileUpload.HasFile)
            {
                try
                {
                    string imgName = FileUpload.FileName;
                    string imgPath = Server.MapPath("~/uploads/");

                    if (!Directory.Exists(imgPath))
                    {
                        Directory.CreateDirectory(imgPath);
                    }

                    string imgFilePath = imgPath + imgName;
                    FileUpload.SaveAs(imgFilePath);

                    byte[] imgArray = File.ReadAllBytes(imgFilePath);
                    string base64 = Convert.ToBase64String(imgArray);

                    ((IRepo)Application["database"]).AddImageForAparment(new Picture
                    {
                        Name = txtImgName.Text,
                        IsRepresentative = cbIsRepresentative.Checked,
                        Base64 = base64
                    }, apartment.ID);

                    File.Delete(imgFilePath);

                    Response.Redirect(Request.Url.LocalPath);
                }
                catch (Exception)
                {
                    // bolji handle greske
                    //Response.StatusCode = 400;
                    //Response.Redirect("/");
                    //Response.End();
                    lblSelectedFileError.Visible = true;
                    lblSelectedFileMessage.Visible = false;
                }
            }
            else
            {
                lblSelectedFileError.Visible = false;
                lblSelectedFileMessage.Visible = true;
            }
        }
    }
}