using B2B.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace B2B
{
    public partial class HostOrder : System.Web.UI.Page
    {
        public OrderController orderController = new OrderController();

        protected void Page_Load(object sender, EventArgs e)
        {
            LoginController loginSystem = new LoginController();
            Host host = loginSystem.GetCurrentUserAccount();
            if (host == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadStatus();
            }
        }

        public void LoadStatus()
        {
            ComboStatus.Items.Clear();
            ComboStatus.Items.Add(new ListItem("Tutti", "0"));
            ComboStatus.Items.Add(new ListItem("Confermati", "1"));
            ComboStatus.Items.Add(new ListItem("Pagati", "2"));
            ComboStatus.Items.Add(new ListItem("Assegnati", "3"));
            ComboStatus.Items.Add(new ListItem("Chiusi", "4"));
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HostOrderEdit.aspx");
        }
    }
}