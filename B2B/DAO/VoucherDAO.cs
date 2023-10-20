using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace B2B.DAO
{
    public class VoucherDAO : BasicDAO
    {
        public VoucherDAO() { }

        public List<Voucher> FindAll()
        {
            Table<Voucher> table = GetContext().Vouchers;
            return table.ToList();
        }

        public Voucher FindById(int id)
        {
            Table<Voucher> table = GetContext().Vouchers;
            return table.Where(
                u =>
                u.Id == id).FirstOrDefault();
        }

        public Voucher FindBySerialNumber(string serialNumber)
        {
            Table<Voucher> table = GetContext().Vouchers;
            return table.Where(
                u =>
                u.SerialNumberGenerator ==  serialNumber).FirstOrDefault();
        }

        public bool Insert(Voucher voucher)
        {
            try
            {
                GetContext().Vouchers.InsertOnSubmit(voucher);
                GetContext().SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }

        public bool Update(Voucher voucher)
        {
            GetContext().SubmitChanges();
            GetContext().Refresh(RefreshMode.OverwriteCurrentValues, voucher);
            return true;
        }
        public bool Delete(int id)
        {
            Voucher voucher = GetContext().Vouchers.SingleOrDefault(u => u.Id == id);
            GetContext().Vouchers.DeleteOnSubmit(voucher);
            GetContext().SubmitChanges();
            return true;
        }

        public IQueryable<Voucher> SearchBy(string search)
        {
            Table<Voucher> table = GetContext().Vouchers;

            IQueryable<Voucher> result = table.Where(u => u.SerialNumberGenerator.Contains(search));
            return result;
        }
    }
}