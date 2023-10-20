using B2B.Controller;
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
    public partial class AdminAccessoryEdit : System.Web.UI.Page
    {
        private int editItemId;
        private Accessory editAccessory;

        private AccessoryController accessoryController = new AccessoryController();
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
                editAccessory = accessoryController.FindBy(editItemId);
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
            if (editAccessory == null) { return; }

            pageTitle.InnerText = "ACCESSORIO (modifica)";
            TxtDescription.Text = editAccessory.Description;

            //Image visible
            string fileName = string.IsNullOrEmpty(editAccessory.Image) ? "Content/Images/accessory_default.jpg" : "Upload/Accessory/" + editAccessory.Image;
            serviceImage.Src = fileName;
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (!IsValid) return;
            bool success = true;

            string description = TxtDescription.Text;
            int id = 0;
            if (editAccessory != null) { id = editAccessory.Id; }

            //Image Save
            string imageTitle = UploadImage();
            if (editAccessory != null && !string.IsNullOrEmpty(editAccessory.Image) && imageTitle == "") { imageTitle = editAccessory.Image; }
            success = accessoryController.SaveAccessory(id, description, imageTitle);

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
                    ImageFile.SaveAs(Server.MapPath("~/Upload/Accessory/" + filename));
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