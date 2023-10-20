using B2B.Common;
using B2B.DAO;
using BusinessLogic.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.WebSockets;

namespace B2B.Controller
{
    public class EncryptedPass
    {
        public string Encrypted { get; set; }
        public string UnEncrypted { get; set; }

    }
    public class LoginController
    {
        private HostDAO hostDao;

        public LoginController()
        {
            hostDao = new HostDAO();
        }

        public LoginCode LoginUserAndSaveSession(string email, EncryptedPass pass)
        {
            string AdminEmail = System.Configuration.ConfigurationManager.AppSettings["AdminUserName"];
            string AdminPW = System.Configuration.ConfigurationManager.AppSettings["AdminPassword"];
            if (email.CompareTo(AdminEmail) == 0 && pass.UnEncrypted.CompareTo(AdminPW) == 0) 
            {
                new SessionController().SetAdmin();
                new SessionController().SetCurrentUserId(0);
                new SessionController().SetCurrentUserEmail(email);
                new SessionController().SetPassword(pass);
                new SessionController().SetTimeout(60 * 24 * 7 * 2);

                return LoginCode.Success;
            }

            Host host = hostDao.FindByEmail(email);
            if (host == null) { return LoginCode.Failed; }
            string userPW = new CryptoController().DecryptStringAES(host.Password);
            if (pass.UnEncrypted.CompareTo(userPW) == 0)
            {
                if (host.IsB2BStaff == true)
                {
                    new SessionController().SetStaff();
                }
                new SessionController().SetCurrentUserId(host.Id);
                new SessionController().SetCurrentUserEmail(host.Email);
                new SessionController().SetPassword(pass);
                new SessionController().SetTimeout(60 * 24 * 7 * 2);

                return LoginCode.Success;
            }
            else
            {
                return LoginCode.Failed;
            }
        }

        public bool IsAdminLoggedIn()
        {
            return new SessionController().GetAdmin() == true;
        }

        public bool IsStaffLoggedIn()
        {
            return new SessionController().GetStaff() == true;
        }

        public Host GetCurrentUserAccount()
        {
            Host host = null;
            int? id = new SessionController().GetCurrentUserId();
            if (id == null) return null;
            host = hostDao.FindById(id.Value);
            return host;
        }

        public bool RegisterUser(string name, string surname, string email, string mobile, string phone, EncryptedPass pass, string note)
        {
            Host host = hostDao.FindByEmail(email);
            if (host != null)
            {
                return false;
            }
            Host registerHost = new Host();
            registerHost.Name = name;
            registerHost.Surname = surname;
            registerHost.Email = email;
            registerHost.Mobile = mobile;
            registerHost.Phone = phone;
            registerHost.Password = pass.Encrypted;
            registerHost.Note = note;
            registerHost.IsB2BStaff = false;

            return hostDao.Insert(registerHost);
        }
    }
}