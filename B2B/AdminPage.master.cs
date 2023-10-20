using B2B.Controller;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace B2B
{
    public partial class AdminPage : System.Web.UI.MasterPage
    {
        public Host host;
        private LoginController loginSystem = new LoginController();
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!loginSystem.IsAdminLoggedIn())
            {
                Response.Redirect("~/Login.aspx");
                return;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SetMenuHighlight();
        }

        protected void SetMenuHighlight()
        {
            string path = HttpContext.Current.Request.Url.AbsolutePath;

            if (path.Equals("/AdminHome.aspx"))
            {
                liOrder.Attributes["class"] += " active";
            }
            else if (path.Equals("/AdminRoom.aspx"))
            {
                liRooms.Attributes["class"] += " active";
            }
            else if (path.Equals("/AdminHost.aspx"))
            {
                liHosts.Attributes["class"] += " active";
            }
            else if (path.Equals("/AdminStaff.aspx"))
            {
                liStaffs.Attributes["class"] += " active";
            }
            else if (path.Equals("/AdminEmployee.aspx"))
            {
                liEmployees.Attributes["class"] += " active";
            }
            else if (path.Equals("/AdminService.aspx"))
            {
                liContents.Attributes["class"] += " active";
                liServices.Attributes["class"] += "active";
            }
            else if (path.Equals("/AdminAccommodation.aspx"))
            {
                liContents.Attributes["class"] += " active";
                liAccommodations.Attributes["class"] += "active";
            }
            else if (path.Equals("/AdminAccessory.aspx"))
            {
                liContents.Attributes["class"] += " active";
                liAccessories.Attributes["class"] += "active";
            }
            else if (path.Equals("/AdminPriceListGroup.aspx"))
            {
                liContents.Attributes["class"] += " active";
                liPriceListGroup.Attributes["class"] += "active";
            }
            else if (path.Equals("/AdminPayment.aspx"))
            {
                liPayments.Attributes["class"] += " active";
                liPaymentResults.Attributes["class"] += "active";
            }
            else if (path.Equals("/AdminVoucher.aspx"))
            {
                liPayments.Attributes["class"] += " active";
                liVouchers.Attributes["class"] += "active";
            }

            username.InnerText = System.Configuration.ConfigurationManager.AppSettings["AdminUserName"];
        }
    }
}