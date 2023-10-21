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
    public partial class AdminPriceListGroupEdit : System.Web.UI.Page
    {
        private int editItemId;
        private PriceListGroup priceListGroup;

        private PriceListGroupController priceListGroupController = new PriceListGroupController();
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
                priceListGroup = priceListGroupController.FindBy(editItemId);
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
            if (priceListGroup == null) { return; }

            pageTitle.InnerText = "LISTINO PREZZI (modifica)";
            TxtDescription.Text = priceListGroup.DescriptionShort;
            TxtPercent.Text = priceListGroup.Percentuale.ToString();            
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (!IsValid) return;
            bool success = true;

            string description = TxtDescription.Text;
            double percent = ParseUtil.TryParseFloat(TxtPercent.Text) ?? 0;
            int id = 0;
            if (priceListGroup != null) { id = priceListGroup.Id; }

            success = priceListGroupController.SavePriceListGroup(id, description, percent);

            if (!success)
            {
                ServerValidator.IsValid = false;
                return;
            }
            Response.Redirect(BtnCancel.PostBackUrl);
        }
    }
}