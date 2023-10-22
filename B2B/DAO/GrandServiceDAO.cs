using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace B2B.DAO
{
    public class GrandServiceDAO : BasicDAO
    {
        public GrandServiceDAO() { }
        public List<GrandService> FindAll()
        {
            Table<GrandService> table = GetContext().GrandServices;
            return table.ToList();
        }

        public GrandService FindById(int id)
        {
            Table<GrandService> table = GetContext().GrandServices;
            return table.Where(
                u =>
                u.Id == id).FirstOrDefault();
        }


        public bool Insert(GrandService grandService)
        {
            try
            {
                GetContext().GrandServices.InsertOnSubmit(grandService);
                GetContext().SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }

        public bool Update(GrandService grandService)
        {
            GetContext().SubmitChanges();
            GetContext().Refresh(RefreshMode.OverwriteCurrentValues, grandService);
            return true;
        }
        public bool Delete(int id)
        {
            GrandService grandService = GetContext().GrandServices.SingleOrDefault(u => u.Id == id);
            GetContext().GrandServices.DeleteOnSubmit(grandService);
            GetContext().SubmitChanges();
            return true;
        }

        public IQueryable<GrandService> SearchBy(string search)
        {
            Table<GrandService> table = GetContext().GrandServices;

            IQueryable<GrandService> result = table.Where(u => u.Title.Contains(search));
            return result;
        }
    }
}