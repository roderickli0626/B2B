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
    public partial class StaffHostEdit : System.Web.UI.Page
    {
        private int editItemId;
        private Host editHost;

        HostController hostController = new HostController();
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
                editHost = hostController.FindBy(editItemId);
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
            if (editHost == null) { return; }

            pageTitle.InnerText = "Cliente (modifica)";
            TxtName.Text = editHost.Name;
            TxtSurname.Text = editHost.Surname;
            TxtEmail.Text = editHost.Email;
            TxtMobile.Text = editHost.Mobile;
            TxtPhone.Text = editHost.Phone;
            TxtNote.Text = editHost.Note;
            TxtPassword.Text = new CryptoController().DecryptStringAES(editHost.Password);
            TxtRepeatPW.Text = TxtPassword.Text;
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (!IsValid) return;

            string name = TxtName.Text;
            string surname = TxtSurname.Text;
            string email = TxtEmail.Text;
            string mobile = TxtMobile.Text;
            string phone = TxtPhone.Text;
            string note = TxtNote.Text;
            string password = TxtPassword.Text;
            string repeat = TxtRepeatPW.Text;
            int id = 0;
            if (editHost != null) id = editHost.Id;
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

            bool success = hostController.SaveHost(id, name, surname, email, mobile, phone, password, note);
            if (success) { Response.Redirect("StaffHost.aspx"); }
            else
            {
                ServerValidator.IsValid = false;
                return;
            }
        }
    }
}