using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace B2B.DAO
{
    public class OrderServiceAllocDAO : BasicDAO
    {
        public OrderServiceAllocDAO() { }

        public bool Insert(OrderServiceAlloc orderServiceAlloc)
        {
            GetContext().OrderServiceAllocs.InsertOnSubmit(orderServiceAlloc);
            GetContext().SubmitChanges();
            return true;
        }

        public bool InsertByOrder(List<OrderServiceAlloc> orderServiceAllocList)
        {
            GetContext().OrderServiceAllocs.InsertAllOnSubmit(orderServiceAllocList);
            GetContext().SubmitChanges();
            return true;
        }

        public void Delete(OrderServiceAlloc orderServiceAlloc)
        {
            GetContext().OrderServiceAllocs.DeleteOnSubmit(orderServiceAlloc);
            GetContext().SubmitChanges();
        }

        public void DeleteByOrder(int orderId)
        {
            IEnumerable<OrderServiceAlloc> orders = GetContext().OrderServiceAllocs.Where(o => o.OrderId == orderId);
            GetContext().OrderServiceAllocs.DeleteAllOnSubmit(orders);
            GetContext().SubmitChanges();
        }

        public IEnumerable<OrderServiceAlloc> FindByOrder(int orderId)
        {
            Table<OrderServiceAlloc> table = GetContext().OrderServiceAllocs;

            IQueryable<OrderServiceAlloc> result;

            result = table.Where(
                    u =>
                    u.OrderId == orderId);
            return result;
        }
    }
}