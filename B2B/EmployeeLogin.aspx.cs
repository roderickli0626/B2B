using B2B.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace B2B
{
    public partial class EmployeeLogin : System.Web.UI.Page
    {
        private EmployeeController employeeController = new EmployeeController();
        private Employment employment = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                TxtName.Text = string.Empty;
                TxtPassword.Text = string.Empty;
            }
        }

        protected void LogInBtn_Click(object sender, EventArgs e)
        {
            if (!IsValid) return;

            string name = TxtName.Text;
            string password = TxtPassword.Text;

            employment = employeeController.FindByNamePassword(name, password);
            if (employment == null)
            {
                ServerValidator.IsValid = false;
                return;
            }
            else
            {
                Response.Redirect("~/EmployeeView.aspx?id=" + employment.Id);
            }
        }
    }
}