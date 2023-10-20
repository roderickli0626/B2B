﻿using B2B.Controller;
using B2B.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace B2B
{
    public partial class AdminAccommodationEdit : System.Web.UI.Page
    {
        private int editItemId;
        private Accomodation editAccommodation;

        private AccommodationController accommodationController = new AccommodationController();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginController loginSystem = new LoginController();
            if (!loginSystem.IsAdminLoggedIn())
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            editItemId = ParseUtil.TryParseInt(Request.Params["id"]) ?? 0;
            if (editItemId != 0)
            {
                editAccommodation = accommodationController.FindBy(editItemId);
            }

            if (!IsPostBack)
            {
                if (editItemId != 0)
                {
                    LoadInfo();
                }
            }
        }

        private void LoadInfo()
        {
            if (editAccommodation == null) { return; }

            pageTitle.InnerText = "SISTEMAZIONE (modifica)";
            TxtDescription.Text = editAccommodation.Description;

            //Image visible
            string fileName = string.IsNullOrEmpty(editAccommodation.Image) ? "Content/Images/accommodation_default.jpg" : "Upload/Accommodation/" + editAccommodation.Image;
            serviceImage.Src = fileName;
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (!IsValid) return;
            bool success = true;

            string description = TxtDescription.Text;
            int id = 0;
            if (editAccommodation != null) { id = editAccommodation.Id; }

            //Image Save
            string imageTitle = UploadImage();
            if (editAccommodation != null && !string.IsNullOrEmpty(editAccommodation.Image) && imageTitle == "") { imageTitle = editAccommodation.Image; }
            success = accommodationController.SaveAccommodation(id, description, imageTitle);

            if (!success)
            {
                ServerValidator.IsValid = false;
                return;
            }
            Response.Redirect(BtnCancel.PostBackUrl);
        }

        private string UploadImage()
        {
            if (ImageFile.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName(ImageFile.FileName);
                    ImageFile.SaveAs(Server.MapPath("~/Upload/Accommodation/" + filename));
                    return filename;
                }
                catch (Exception ex)
                {
                }
            }
            return "";
        }
    }
}