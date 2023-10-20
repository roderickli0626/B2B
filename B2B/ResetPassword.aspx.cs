using B2B.Controller;
using B2B.DAO;
using BusinessLogic.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.WebSockets;

namespace B2B
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        private string email;
        private string token;
        private HostDAO hostDao= new HostDAO();
        protected void Page_Load(object sender, EventArgs e)
        {
            email = Request.Params["email"];
            token = Request.Params["token"];
            if (email == null || token ==  null)
            {
                Response.Redirect("~/Login.aspx");
            }
            Host host = hostDao.FindByEmail(email);
            if (host == null) Response.Redirect("~/Login.aspx");
            string hostToken = host.ResetToken;
            DateTime? tokenExpire = host.ResetTokenExpiry;
            DateTime currentDateTime = DateTime.Now;
            if (tokenExpire < currentDateTime || hostToken != token) Response.Redirect("~/Login.aspx");
        }

        protected void ResetPassword_Click(object sender, EventArgs e)
        {
            if (!IsValid) return;
            string password = TxtPassword.Text.Trim();
            string repeatPW = TxtRepeatPW.Text.Trim();

            if (password != repeatPW)
            {
                ServerValidator.IsValid = false; return;
            }

            Host host = hostDao.FindByEmail(email);
            if (host == null) { return; }
            if (!string.IsNullOrEmpty(password)) host.Password = new CryptoController().EncryptStringAES(password);
            hostDao.Update(host);
            Response.Redirect("~/Login.aspx");
        }
    }
}