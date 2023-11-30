using B2B.Controller;
using B2B.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace B2B
{
    public partial class StaffGrandServiceEdit : System.Web.UI.Page
    {
        private int editItemId;
        private GrandService grandService;

        private GrandServiceController grandServiceController = new GrandServiceController();
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
                grandService = grandServiceController.FindBy(editItemId);
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
            if (grandService == null) { return; }

            pageTitle.InnerText = "CATEGORIA SERVIZI (modifica)";
            TxtTitle.Text = grandService.Title;
            TxtDescription.Text = grandService.Description;
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (!IsValid) return;
            bool success = true;

            string description = TxtDescription.Text;
            string title = TxtTitle.Text;
            int id = 0;
            if (grandService != null) { id = grandService.Id; }

            success = grandServiceController.SaveGrandService(id, title, description);

            if (!success)
            {
                ServerValidator.IsValid = false;
                return;
            }
            Response.Redirect(BtnCancel.PostBackUrl);
        }
    }
}