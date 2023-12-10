using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace B2B.DAO
{
    public class OrderDAO : BasicDAO
    {
        public OrderDAO() { }

        public List<Order> FindAll()
        {
            Table<Order> table = GetContext().Orders;
            return table.ToList();
        }

        public Order FindById(int id)
        {
            Table<Order> table = GetContext().Orders;
            return table.Where(
                u =>
                u.Id == id).FirstOrDefault();
        }

        public List<Order> FindByHost(int hostId)
        {
            return GetContext().Orders.Where(u => u.HostId == hostId).ToList();
        }

        public List<Order> FindByRoom(int roomId)
        {
            return GetContext().Orders.Where(u => u.RoomId == roomId).ToList();
        }

        public bool Insert(Order order)
        {
            try
            {
                GetContext().Orders.InsertOnSubmit(order);
                GetContext().SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }

        public int? Insert2(Order order)
        {
            try
            {
                GetContext().Orders.InsertOnSubmit(order);
                GetContext().SubmitChanges();
                return order.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public bool Update(Order order)
        {
            GetContext().SubmitChanges();
            GetContext().Refresh(RefreshMode.OverwriteCurrentValues, order);
            return true;
        }
        public bool Delete(int id)
        {
            Order order = GetContext().Orders.SingleOrDefault(u => u.Id == id);
            GetContext().Orders.DeleteOnSubmit(order);
            GetContext().SubmitChanges();
            return true;
        }

        public IQueryable<Order> SearchBy(DateTime? from, DateTime? to, string search, int status)
        {
            Table<Order> table = GetContext().Orders;

            IQueryable<Order> result = table.Where(
                u =>
                u.Host.Name.Contains(search));
            if (status != 0)
                result = result.Where(d => d.Status == status);

            if (from != null)
                result = result.Where(u => u.StartDate >= from.Value);

            if (to != null)
                result = result.Where(u => u.EndDate <= to.Value);

            return result;
        }

        public IQueryable<Order> SearchByHost(int hostId, DateTime? from, DateTime? to, string search, int status)
        {
            IQueryable<Order> result = GetContext().Orders.Where(r => r.HostId == hostId);
            result = result.Where(
                u =>
                u.Host.Name.Contains(search));
                //u.Note.Contains(search));
            if (status != 0)
                result = result.Where(d => d.Status == status);

            if (from != null)
                result = result.Where(u => u.StartDate >= from.Value);

            if (to != null)
                result = result.Where(u => u.EndDate <= to.Value);

            return result;
        }

        public IQueryable<Order> SearchByEmployee(int employeeID, DateTime? from, DateTime? to, string search, int status)
        {
            try
            {
                Table<Order> table = GetContext().Orders;

                IQueryable<Order> result = table.Where(r => r.EmploymentId.Contains(employeeID.ToString()));
                result = result.Where(
                    u =>
                    u.Host.Name.Contains(search));
                if (status != 0)
                    result = result.Where(d => d.Status == status);

                if (from != null)
                    result = result.Where(u => u.StartDate >= from.Value);

                if (to != null)
                    result = result.Where(u => u.EndDate <= to.Value);

                return result;
            }
            catch (Exception ex) 
            { 
                string ere = ex.ToString();
                Console.WriteLine(ere);
            }
            

            return null;
        }
    }
}