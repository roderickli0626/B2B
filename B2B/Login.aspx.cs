using B2B.Common;
using B2B.Controller;
using BusinessLogic.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace B2B
{
    public partial class Login : System.Web.UI.Page
    {
        LoginController loginSystem = new LoginController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SessionController sessionController = new SessionController();
                sessionController.LogoutUser();
            }
        }        
        protected void LogInBtn_Click(object sender, EventArgs e)
        {
            string email = TxtEmail.Text;
            string password = TxtPassword.Text;
            EncryptedPass pass = new EncryptedPass() { UnEncrypted = password, Encrypted = new CryptoController().EncryptStringAES(password) };

            LoginUser(email, pass);
        }
        private void LoginUser(string email, EncryptedPass pass)
        {
            if (!IsValid) return;

            LoginCode loginStatus = loginSystem.LoginUserAndSaveSession(email, pass);

            if (loginStatus == LoginCode.Success)
            {
                if (loginSystem.IsAdminLoggedIn())
                {
                    Response.Redirect("~/AdminHome.aspx");
                }
                else if (loginSystem.IsStaffLoggedIn())
                {
                    Response.Redirect("~/StaffHome.aspx");
                }
                else
                {
                    Response.Redirect("~/HostHome.aspx");
                }
            }
            else
            {
                ServerValidator.IsValid = false;
                return;
            }

        }
    }
}