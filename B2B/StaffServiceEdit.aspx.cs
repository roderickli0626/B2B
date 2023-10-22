using B2B.Controller;
using B2B.DAO;
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
    public partial class StaffServiceEdit : System.Web.UI.Page
    {
        private int editItemId;
        private Service editService;

        private ServiceController serviceController = new ServiceController();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginController loginSystem = new LoginController();
            if (!loginSystem.IsStaffLoggedIn())
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            editItemId = ParseUtil.TryParseInt(Request.Params["id"]) ?? 0;
            if (editItemId != 0)
            {
                editService = serviceController.FindBy(editItemId);
            }

            if (!IsPostBack)
            {
                LoadGrandService();
                if (editItemId != 0)
                {
                    LoadInfo();
                }
            }
        }
        private void LoadGrandService()
        {
            List<GrandService> grandServiceList = new GrandServiceDAO().FindAll();
            ControlUtil.DataBind(ComboGrandService, grandServiceList, "Id", "Title", 0, "");
        }
        private void LoadInfo()
        {
            if (editService == null) { return; }

            pageTitle.InnerText = "SERVIZIO (modifica) ";
            TxtTitle.Text = editService.DescriptionShort;
            TxtDescription.Text = editService.DescriptionLong;
            TxtPrice.Text = editService.Price.ToString();
            ControlUtil.SelectValue(ComboGrandService, editService.GrandServiceID);

            if (editService.HavePriceGroup == true) RadioButton1.Checked = true;
            else RadioButton2.Checked = true;

            //Image visible
            string fileName = string.IsNullOrEmpty(editService.Image) ? "Content/Images/service_default.jpg" : "Upload/Service/" + editService.Image;
            serviceImage.Src = fileName;
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (!IsValid) return;
            bool success = true;

            string descriptionShort = TxtTitle.Text;
            string descriptionLong = TxtDescription.Text;
            double price = ParseUtil.TryParseDouble(TxtPrice.Text) ?? 0;
            int? grandServiceID = ControlUtil.GetSelectedValue(ComboGrandService);
            if (grandServiceID == null)
            {
                ServerValidator0.IsValid = false;
                return;
            }

            bool havePriceGroup = false;

            if (RadioButton1.Checked) havePriceGroup = true;
            else if (RadioButton2.Checked) havePriceGroup = false;

            int id = 0;
            if (editService != null) { id = editService.Id; }

            //Image Save
            string imageTitle = UploadImage();
            if (editService != null && !string.IsNullOrEmpty(editService.Image) && imageTitle == "") { imageTitle = editService.Image; }
            success = serviceController.SaveService(id, descriptionShort, descriptionLong, price, imageTitle, havePriceGroup, grandServiceID);

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
                    ImageFile.SaveAs(Server.MapPath("~/Upload/Service/" + filename));
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