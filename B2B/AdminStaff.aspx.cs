using B2B.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace B2B
{
    public partial class AdminStaff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginController loginSystem = new LoginController();
            if (!loginSystem.IsAdminLoggedIn())
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadSearchKey();
            }
        }

        private void LoadSearchKey()
        {
            ComboSearchKey.Items.Clear();
            ComboSearchKey.Items.Add(new ListItem("Nome", "1"));
            ComboSearchKey.Items.Add(new ListItem("Cognome", "2"));
            ComboSearchKey.Items.Add(new ListItem("E-mail", "3"));
            ComboSearchKey.Items.Add(new ListItem("Telefono", "4"));
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdminStaffEdit.aspx");
        }
    }
}