using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace B2B.DAO
{
    public class ServiceDAO : BasicDAO
    {
        public ServiceDAO() { }

        public List<Service> FindAll()
        {
            Table<Service> table = GetContext().Services;
            return table.ToList();
        }

        public Service FindById(int id)
        {
            Table<Service> table = GetContext().Services;
            return table.Where(
                u =>
                u.Id == id).FirstOrDefault();
        }
        public List<Service> FindByGrandService(int grandServcieID)
        {
            Table<Service> table = GetContext().Services;
            return table.Where(
                u =>
                u.GrandServiceID == grandServcieID).ToList();
        }

        public bool Insert(Service service)
        {
            try
            {
                GetContext().Services.InsertOnSubmit(service);
                GetContext().SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }

        public bool Update(Service service)
        {
            GetContext().SubmitChanges();
            GetContext().Refresh(RefreshMode.OverwriteCurrentValues, service);
            return true;
        }
        public bool Delete(int id)
        {
            Service service = GetContext().Services.SingleOrDefault(u => u.Id == id);
            GetContext().Services.DeleteOnSubmit(service);
            GetContext().SubmitChanges();
            return true;
        }

        public IQueryable<Service> SearchBy(string search, int grandServiceID)
        {
            Table<Service> table = GetContext().Services;

            IQueryable<Service> result = table.Where(u => u.DescriptionShort.Contains(search));
            if (grandServiceID != 0)
            {
                result = result.Where(r => r.GrandServiceID == grandServiceID);
            }
            return result;
        }
    }
}