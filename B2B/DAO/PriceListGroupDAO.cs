using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace B2B.DAO
{
    public class PriceListGroupDAO : BasicDAO
    {
        public PriceListGroupDAO()
        {
        }

        public List<PriceListGroup> FindAll()
        {
            Table<PriceListGroup> table = GetContext().PriceListGroups;
            return table.ToList();
        }

        public PriceListGroup FindById(int id)
        {
            Table<PriceListGroup> table = GetContext().PriceListGroups;
            return table.Where(
                u =>
                u.Id == id).FirstOrDefault();
        }


        public bool Insert(PriceListGroup priceListGroup)
        {
            try
            {
                GetContext().PriceListGroups.InsertOnSubmit(priceListGroup);
                GetContext().SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }

        public bool Update(PriceListGroup priceListGroup)
        {
            GetContext().SubmitChanges();
            GetContext().Refresh(RefreshMode.OverwriteCurrentValues, priceListGroup);
            return true;
        }
        public bool Delete(int id)
        {
            PriceListGroup priceListGroup = GetContext().PriceListGroups.SingleOrDefault(u => u.Id == id);
            GetContext().PriceListGroups.DeleteOnSubmit(priceListGroup);
            GetContext().SubmitChanges();
            return true;
        }

        public IQueryable<PriceListGroup> SearchBy(string search)
        {
            Table<PriceListGroup> table = GetContext().PriceListGroups;

            IQueryable<PriceListGroup> result = table.Where(u => u.DescriptionShort.Contains(search));
            return result;
        }
    }
}