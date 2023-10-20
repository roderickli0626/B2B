using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace B2B.DAO
{
    public class HostDAO : BasicDAO
    {
        public HostDAO() { }

        public List<Host> FindAll()
        {
            Table<Host> table = GetContext().Hosts;
            return table.ToList();
        }

        public List<Host> FindStaff()
        {
            Table<Host> hosts = GetContext().Hosts;
            List<Host> result = hosts.Where(
                u => 
                u.IsB2BStaff == true).ToList();
            return result;
        }

        public Host FindById(int id)
        {
            Table<Host> table = GetContext().Hosts;
            return table.Where(
                u =>
                u.Id == id).FirstOrDefault();
        }

        public Host FindByEmail(string email)
        {
            return GetContext().Hosts.SingleOrDefault(u => u.Email == email);
        }

        public bool Insert(Host host)
        {
            try
            {
                GetContext().Hosts.InsertOnSubmit(host);
                GetContext().SubmitChanges();
                return true;
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex);
            }
            return false;
        }

        public bool Update(Host host)
        {
            GetContext().SubmitChanges();
            GetContext().Refresh(RefreshMode.OverwriteCurrentValues, host);
            return true;
        }
        public bool Delete(int id)
        {
            Host host = GetContext().Hosts.SingleOrDefault(u => u.Id == id);
            GetContext().Hosts.DeleteOnSubmit(host);
            GetContext().SubmitChanges();
            return true;
        }

        public IQueryable<Host> SearchHostsBy(string search, int searchKey)
        {
            Table<Host> table = GetContext().Hosts;

            IQueryable<Host> result = table.Where(u => u.IsB2BStaff == false);
            if (searchKey == 1)
                result = result.Where(d => d.Name.Contains(search));

            if (searchKey == 2)
                result = result.Where(u => u.Surname.Contains(search));

            if (searchKey == 3)
                result = result.Where(u => u.Email.Contains(search));

            if (searchKey == 4)
                result = result.Where(u => u.Mobile.Contains(search) || u.Phone.Contains(search));

            return result;
        }

        public IQueryable<Host> SearchStaffsBy(string search, int searchKey)
        {
            Table<Host> table = GetContext().Hosts;

            IQueryable<Host> result = table.Where(u => u.IsB2BStaff == true);
            if (searchKey == 1)
                result = result.Where(d => d.Name.Contains(search));

            if (searchKey == 2)
                result = result.Where(u => u.Surname.Contains(search));

            if (searchKey == 3)
                result = result.Where(u => u.Email.Contains(search));

            if (searchKey == 4)
                result = result.Where(u => u.Mobile.Contains(search) || u.Phone.Contains(search));

            return result;
        }
    }
}