using B2B.Controller;
using B2B.Util;
using BusinessLogic.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace B2B
{
    public partial class AdminStaffEdit : System.Web.UI.Page
    {
        private int editItemId;
        private Host editStaff;

        HostController hostController = new HostController();
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
                editStaff = hostController.FindBy(editItemId);
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
            if (editStaff == null) { return; }

            pageTitle.InnerText = "STAFF (modifica) ";
            TxtName.Text = editStaff.Name;
            TxtSurname.Text = editStaff.Surname;
            TxtStaffEmail.Text = editStaff.Email;
            TxtMobile.Text = editStaff.Mobile;
            TxtPhone.Text = editStaff.Phone;
            TxtNote.Text = editStaff.Note;
            TxtStaffPassword.Text = new CryptoController().DecryptStringAES(editStaff.Password);
            TxtStaffRepeatPW.Text = TxtStaffPassword.Text;
        }


        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (!IsValid) return;

            string name = TxtName.Text;
            string surname = TxtSurname.Text;
            string email = TxtStaffEmail.Text;
            string mobile = TxtMobile.Text;
            string phone = TxtPhone.Text;
            string note = TxtNote.Text;
            string password = TxtStaffPassword.Text;
            string repeat = TxtStaffRepeatPW.Text;
            int id = 0;
            if (editStaff != null) id = editStaff.Id;
            else
            {
                if (string.IsNullOrEmpty(password))
                {
                    PasswordValidator1.IsValid = false;
                    return;
                }
            }

            if (password != repeat)
            {
                PasswordValidator.IsValid = false;
                return;
            }

            string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            if (!Regex.IsMatch(email, emailPattern))
            {
                EmailValidator.IsValid = false;
                return;
            }

            bool success = hostController.SaveStaff(id, name, surname, email, mobile, phone, password, note);
            if (success) { Response.Redirect("AdminStaff.aspx"); }
            else
            {
                ServerValidator.IsValid = false;
                return;
            }
        }
    }
}