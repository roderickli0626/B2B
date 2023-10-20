using B2B.Controller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace B2B
{
    public partial class HostPage : System.Web.UI.MasterPage
    {
        public Host host;
        private LoginController loginSystem = new LoginController();
        protected void Page_Init(object sender, EventArgs e)
        {
            host = loginSystem.GetCurrentUserAccount();

            if (host == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = HttpContext.Current.Request.Url.AbsolutePath;

            if (path.Equals("/HostHome.aspx"))
            {
                liRooms.Attributes["class"] += " active";
            }
            else if (path.Equals("/HostOrder.aspx"))
            {
                liOrder.Attributes["class"] += " active";
            }

            username.InnerText = host.Name;
        }
    }
}