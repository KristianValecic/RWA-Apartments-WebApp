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
    public partial class ImageControl : System.Web.UI.UserControl
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (lblRep.Text == "True" || lblRep.Text == "true")
            {
                lblRep.Text = "Representative";
            }
            else
            {
                lblRep.Text = "";
            }
        }

        protected void deleteImg_Click(object sender, EventArgs e)
        {
            ((IRepo)Application["database"]).DeleteImg(int.Parse(ImgID.Value));
            Response.Redirect(Request.Url.LocalPath);
        }
    }
}