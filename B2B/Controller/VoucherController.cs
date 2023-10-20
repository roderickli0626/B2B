using B2B.DAO;
using B2B.Model;
using B2B.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B2B.Controller
{
    public class VoucherController
    {
        VoucherDAO voucherDAO;
        public VoucherController() 
        { 
            voucherDAO = new VoucherDAO();
        }

        public SearchResult SearchBy(int start, int length, string search)
        {
            SearchResult result = new SearchResult();
            IQueryable<Voucher> list = voucherDAO.SearchBy(search).OrderBy(l => l.Id);
            result.TotalCount = list.Count();
            list = list.Skip(start).Take(length);

            List<object> checks = new List<object>();
            foreach (Voucher fb in list)
            {
                VoucherCheck check = new VoucherCheck(fb);
                List<Order> orders = fb.Orders.ToList();
                if (orders.Count > 0)
                {
                    string summary = "";
                    double total = fb.Amount ?? 0;
                    string owner = "";
                    foreach (Order order in orders)
                    {
                        string paydate = order.Payment.DateOfPay?.ToString("dd/MM/yyyy");
                        string hostName = order.Host.Name;
                        owner = hostName;
                        string amount = order.Payment.Note.Split(',')[0].Split(':')[1].Trim();
                        total += ParseUtil.TryParseFloat(amount) ?? 0;
                        summary += paydate + " " + hostName + " with order " + order.Id + " amount " + amount + " €\n"; 
                    }
                    check.Summary = "- Voucher of " + total + " € -\n" + summary;
                    check.Owner = owner;
                }
                checks.Add(check);
            }
            result.ResultList = checks;

            return result;
        }

        public bool DeleteVoucher(int id)
        {
            Voucher item = voucherDAO.FindById(id);
            if (item == null) return false;

            return voucherDAO.Delete(id);
        }

        public Voucher FindBy(int id)
        {
            return voucherDAO.FindById(id);
        }

        public bool SaveVoucher(int id, string note, string serialNumber, double amount)
        {
            if (id == 0)
            {
                Voucher voucher = new Voucher();
                voucher.Note = note;
                voucher.SerialNumberGenerator = serialNumber;
                voucher.Amount = amount;

                return voucherDAO.Insert(voucher);
            }
            else
            {
                Voucher voucher = voucherDAO.FindById(id);
                voucher.Note = note;
                voucher.SerialNumberGenerator = serialNumber;
                voucher.Amount = amount;

                return voucherDAO.Update(voucher);
            }
        }
    }
}