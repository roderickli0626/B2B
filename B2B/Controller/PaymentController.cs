using B2B.DAO;
using B2B.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B2B.Controller
{
    public class PaymentController
    {
        PaymentDAO paymentDAO;
        public PaymentController()
        {
            paymentDAO = new PaymentDAO();
        }

        public SearchResult SearchPayments(DateTime? dateFrom, DateTime? dateTo, int start, int length, string search, int method)
        {
            SearchResult result = new SearchResult();
            IQueryable<Payment> list = paymentDAO.SearchBy(dateFrom, dateTo, search, method).OrderByDescending(l => l.DateOfPay);
            result.TotalCount = list.Count();
            list = list.Skip(start).Take(length);

            List<object> checks = new List<object>();
            foreach (Payment fb in list)
            {
                PaymentCheck check = new PaymentCheck(fb);
                checks.Add(check);
            }
            result.ResultList = checks;

            return result;
        }

        public bool DeletePayment(int id)
        {
            Payment item = paymentDAO.FindById(id);
            if (item == null) return false;

            return paymentDAO.Delete(id);
        }

        public Payment FindBy(int id)
        {
            return paymentDAO.FindById(id);
        }

        public bool SavePayment(int id, DateTime date, int orderId, double amount, string paypalTransitionID, string note)
        {
            if (id == 0)
            {
                Payment payment = new Payment();
                payment.DateOfPay = date;
                payment.OrderId = orderId;
                payment.PaypalTransitionID = paypalTransitionID;
                payment.Amount = amount;
                payment.Note = note;

                return paymentDAO.Insert(payment);
            }
            else
            {
                Payment payment = paymentDAO.FindById(id);
                payment.DateOfPay = date;
                payment.OrderId = orderId;
                payment.PaypalTransitionID = paypalTransitionID;
                payment.Amount = amount;
                payment.Note = note;

                return paymentDAO.Update(payment);
            }
        } 
    }
}