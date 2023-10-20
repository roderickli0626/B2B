using B2B.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace B2B
{
    public partial class StaffPage : System.Web.UI.MasterPage
    {
        public Host host;
        private LoginController loginSystem = new LoginController();
        protected void Page_Init(object sender, EventArgs e)
        {
            host = loginSystem.GetCurrentUserAccount();

            if (!loginSystem.IsStaffLoggedIn() || host == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = HttpContext.Current.Request.Url.AbsolutePath;

            if (path.Equals("/StaffHome.aspx"))
            {
                liOrder.Attributes["class"] += " active";
            }
            else if (path.Equals("/StaffRoom.aspx"))
            {
                liRooms.Attributes["class"] += " active";
            }
            else if (path.Equals("/StaffHost.aspx"))
            {
                liHosts.Attributes["class"] += " active";
            }
            else if (path.Equals("/StaffEmployee.aspx"))
            {
                liEmployees.Attributes["class"] += " active";
            }
            else if (path.Equals("/StaffService.aspx"))
            {
                liContents.Attributes["class"] += " active";
                liServices.Attributes["class"] += "active";
            }
            else if (path.Equals("/StaffAccommodation.aspx"))
            {
                liContents.Attributes["class"] += " active";
                liAccommodations.Attributes["class"] += "active";
            }
            else if (path.Equals("/StaffAccessory.aspx"))
            {
                liContents.Attributes["class"] += " active";
                liAccessories.Attributes["class"] += "active";
            }
            else if (path.Equals("/StaffPriceListGroup.aspx"))
            {
                liContents.Attributes["class"] += " active";
                liPriceListGroup.Attributes["class"] += "active";
            }
            else if (path.Equals("/StaffPayment.aspx"))
            {
                liPayments.Attributes["class"] += " active";
                liPaymentResults.Attributes["class"] += "active";
            }
            else if (path.Equals("/StaffVoucher.aspx"))
            {
                liPayments.Attributes["class"] += " active";
                liVouchers.Attributes["class"] += "active";
            }

            username.InnerText = host.Name;
        }
    }
}