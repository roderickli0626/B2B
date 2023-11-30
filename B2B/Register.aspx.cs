using B2B.Controller;
using BusinessLogic.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace B2B
{
    public partial class Register : System.Web.UI.Page
    {
        LoginController loginSystem = new LoginController();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnRegister_Click(object sender, EventArgs e)
        {
            string name = TxtName.Text;
            string surname = TxtSurname.Text;
            string email = TxtRegEmail.Text;
            string password = TxtRegPassword.Text;
            string repeatpw = TxtRegRepeatPW.Text;
            string phone = TxtPhone.Text;
            string mobile = TxtMobile.Text;
            string note = TxtNote.Text;

            EncryptedPass pass = new EncryptedPass() { UnEncrypted = password, Encrypted = new CryptoController().EncryptStringAES(password) };


            if (password != repeatpw)
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

            RegisterUser(name, surname, email, phone, mobile, pass, note);
        }

        public void RegisterUser(string name, string surname, string email, string mobile, string phone, EncryptedPass pass, string note)
        {
            if (!IsValid) { return; }

            bool success = loginSystem.RegisterUser(name, surname, email, mobile, phone, pass, note);
            if (success) {
                //Send Email to Admin
                SendEmail(email);
                Response.Redirect("~/Login.aspx"); 
            }
            else
            {
                ServerValidator.IsValid = false;
                return;
            }
        }

        private void SendEmail(string email)
        {
            //Send Email
            MailMessage Msg = new MailMessage();
            Msg.From = new MailAddress("Krandall2005@gmail.com", "BnB Host");// Sender details here, replace with valid value
            Msg.Subject = "Un nuovo CLIENTE si è registrato"; // subject of email
            Msg.To.Add("Digraziag286@gmail.com"); //Add Email id, to which we will send email
            Msg.Body = email + " è stato aggiunto al gestionale.";
            Msg.IsBodyHtml = true;
            Msg.Priority = MailPriority.High;
            SmtpClient smtp = new SmtpClient();
            smtp.UseDefaultCredentials = false; // to get rid of error "SMTP server requires a secure connection"
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;

            smtp.Credentials = new System.Net.NetworkCredential("krandall2005@gmail.com", "fyjlmiowttdaovfi");// replace with valid value
            smtp.EnableSsl = true;

            smtp.Send(Msg);
        }
    }
}