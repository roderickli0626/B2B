using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace B2B.DAO
{
    public class PaymentDAO : BasicDAO
    {
        public PaymentDAO() { }

        public List<Payment> FindAll()
        {
            Table<Payment> table = GetContext().Payments;
            return table.ToList();
        }

        public Payment FindById(int id)
        {
            Table<Payment> table = GetContext().Payments;
            return table.Where(
                u =>
                u.Id == id).FirstOrDefault();
        }

        public bool Insert(Payment payment)
        {
            try
            {
                GetContext().Payments.InsertOnSubmit(payment);
                GetContext().SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }

        public int? Insert2(Payment payment)
        {
            try
            {
                GetContext().Payments.InsertOnSubmit(payment);
                GetContext().SubmitChanges();
                return payment.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public bool Update(Payment payment)
        {
            GetContext().SubmitChanges();
            GetContext().Refresh(RefreshMode.OverwriteCurrentValues, payment);
            return true;
        }
        public bool Delete(int id)
        {
            Payment payment = GetContext().Payments.SingleOrDefault(u => u.Id == id);
            GetContext().Payments.DeleteOnSubmit(payment);
            GetContext().SubmitChanges();
            return true;
        }

        public IQueryable<Payment> SearchBy(DateTime? from, DateTime? to, string search, int method)
        {
            Table<Payment> table = GetContext().Payments;

            IQueryable<Payment> result = table.Where(
                u =>
                u.PaypalTransitionID.Contains(search) || u.Orders.First().Host.Name.Contains(search));

            if (method != 0)
                result = result.Where(d => d.Method == method);

            if (from != null)
                result = result.Where(u => u.DateOfPay >= from.Value);

            if (to != null)
                result = result.Where(u => u.DateOfPay <= to.Value);

            return result;
        }

    }
}