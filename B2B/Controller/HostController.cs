using B2B.DAO;
using B2B.Model;
using BusinessLogic.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace B2B.Controller
{
    public class HostController
    {
        HostDAO hostDAO;

        public HostController()
        {
            hostDAO = new HostDAO();
        }

        public SearchResult SearchHosts(int start, int length, string search, int searchKey)
        {
            SearchResult result = new SearchResult();
            IQueryable<Host> list = hostDAO.SearchHostsBy(search, searchKey).OrderBy(l => l.Id);
            result.TotalCount = list.Count();
            list = list.Skip(start).Take(length);

            List<object> checks = new List<object>();
            foreach (Host fb in list)
            {
                HostCheck check = new HostCheck(fb);
                checks.Add(check);
            }
            result.ResultList = checks;

            return result;
        }

        public SearchResult SearchStaffs(int start, int length, string search, int searchKey)
        {
            SearchResult result = new SearchResult();
            IQueryable<Host> list = hostDAO.SearchStaffsBy(search, searchKey).OrderBy(l => l.Id);
            result.TotalCount = list.Count();
            list = list.Skip(start).Take(length);

            List<object> checks = new List<object>();
            foreach (Host fb in list)
            {
                HostCheck check = new HostCheck(fb);
                checks.Add(check);
            }
            result.ResultList = checks;

            return result;
        }

        public bool DeleteHost(int id)
        {
            Host item = hostDAO.FindById(id);
            if (item == null) return false;

            return hostDAO.Delete(id);
        }

        public Host FindBy(int id) 
        {
            return hostDAO.FindById(id);
        }

        public bool SaveHost(int id, string name, string surname, string email, string mobile, string phone, string password, string note)
        {
            Host host = null;
            if (id == 0)
            {
                Host host1 = hostDAO.FindByEmail(email);
                if (host1 != null) return false;

                host = new Host();
                host.IsB2BStaff = false;
                host.Name = name;
                host.Surname = surname;
                host.Email = email;
                host.Mobile = mobile;
                host.Phone = phone;
                host.Password = new CryptoController().EncryptStringAES(password);
                host.Note = note;

                return hostDAO.Insert(host);
            }
            else
            {
                Host host1 = hostDAO.FindByEmail(email);
                if (host1 != null && host1.Id != id) return false;

                host = hostDAO.FindById(id);
                if (host == null) return false;
                
                host.IsB2BStaff = false;
                host.Name = name;
                host.Surname = surname;
                host.Email = email;
                host.Mobile = mobile;
                host.Phone = phone;
                if (!string.IsNullOrEmpty(password)) host.Password = new CryptoController().EncryptStringAES(password);
                host.Note = note;

                return hostDAO.Update(host);
            }
        }

        public bool SaveStaff(int id, string name, string surname, string email, string mobile, string phone, string password, string note)
        {
            Host host = null;
            if (id == 0)
            {
                Host host1 = hostDAO.FindByEmail(email);
                if (host1 != null) return false;

                host = new Host();
                host.IsB2BStaff = true;
                host.Name = name;
                host.Surname = surname;
                host.Email = email;
                host.Mobile = mobile;
                host.Phone = phone;
                host.Password = new CryptoController().EncryptStringAES(password);
                host.Note = note;

                return hostDAO.Insert(host);
            }
            else
            {
                Host host1 = hostDAO.FindByEmail(email);
                if (host1 != null && host1.Id != id) return false;

                host = hostDAO.FindById(id);
                if (host == null) return false;

                host.IsB2BStaff = true;
                host.Name = name;
                host.Surname = surname;
                host.Email = email;
                host.Mobile = mobile;
                host.Phone = phone;
                if (!string.IsNullOrEmpty(password)) host.Password = new CryptoController().EncryptStringAES(password);
                host.Note = note;

                return hostDAO.Update(host);
            }
        }
    }
}