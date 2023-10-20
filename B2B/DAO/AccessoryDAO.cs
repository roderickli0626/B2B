using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace B2B.DAO
{
    public class AccessoryDAO : BasicDAO
    {
        public AccessoryDAO() { }

        public List<Accessory> FindAll()
        {
            Table<Accessory> table = GetContext().Accessories;
            return table.ToList();
        }

        public Accessory FindById(int id)
        {
            Table<Accessory> table = GetContext().Accessories;
            return table.Where(
                u =>
                u.Id == id).FirstOrDefault();
        }


        public bool Insert(Accessory accessory)
        {
            try
            {
                GetContext().Accessories.InsertOnSubmit(accessory);
                GetContext().SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }

        public bool Update(Accessory accessory)
        {
            GetContext().SubmitChanges();
            GetContext().Refresh(RefreshMode.OverwriteCurrentValues, accessory);
            return true;
        }
        public bool Delete(int id)
        {
            Accessory accessory = GetContext().Accessories.SingleOrDefault(u => u.Id == id);
            GetContext().Accessories.DeleteOnSubmit(accessory);
            GetContext().SubmitChanges();
            return true;
        }

        public IQueryable<Accessory> SearchBy(string search)
        {
            Table<Accessory> table = GetContext().Accessories;

            IQueryable<Accessory> result = table.Where(u => u.Description.Contains(search));
            return result;
        }
    }
}