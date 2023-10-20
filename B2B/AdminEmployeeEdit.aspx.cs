using B2B.Controller;
using B2B.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace B2B
{
    public partial class AdminEmployeeEdit : System.Web.UI.Page
    {
        private int editItemId;
        private Employment editEmployee;

        EmployeeController employeeController = new EmployeeController();
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
                editEmployee = employeeController.FindBy(editItemId);
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
            if (editEmployee == null) { return; }

            pageTitle.InnerText = "COLLABORATORI (modifica)";
            TxtName.Text = editEmployee.Name;
            TxtSurname.Text = editEmployee.Surname;
            TxtMobile.Text = editEmployee.MobilePhone;
            TxtNote.Text = editEmployee.Note;
            TxtPassword.Text = editEmployee.Password;
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (!IsValid) return;

            string name = TxtName.Text;
            string surname = TxtSurname.Text;
            string mobile = TxtMobile.Text;
            string note = TxtNote.Text;
            string password = TxtPassword.Text;
            int id = 0;
            if (editEmployee != null) { id =  editEmployee.Id; }

            bool success = employeeController.SaveEmployee(id, name, surname, mobile, note, password);
            if (success) { Response.Redirect("AdminEmployee.aspx"); }
            else
            {
                ServerValidator.IsValid = false;
                return;
            }
        }
    }
}