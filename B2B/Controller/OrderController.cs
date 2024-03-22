using B2B.DAO;
using B2B.Model;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B2B.Controller
{
    public class OrderController
    {
        public OrderDAO orderDao;
        public OrderServiceAllocDAO allocDao;
        public EmploymentDAO employmentDao = new EmploymentDAO();
        public RoomDAO roomDao = new RoomDAO();

        public OrderController()
        {
            orderDao = new OrderDAO();
            allocDao = new OrderServiceAllocDAO();
        }

        public SearchResult SearchOrders(DateTime? dateFrom, DateTime? dateTo, int start, int length, string search, int status)
        {
            SearchResult result = new SearchResult();
            IQueryable<Order> list = orderDao.SearchBy(dateFrom, dateTo, search, status).OrderByDescending(l => l.StartDate);
            result.TotalCount = list.Count();
            list = list.Skip(start).Take(length);

            List<object> checks = new List<object>();
            foreach (Order fb in list)
            {
                OrderCheck check = new OrderCheck(fb);
                if (fb.EmploymentId != null)
                {
                    List<Employment> employees = new EmployeeController().FindByIDS(fb.EmploymentId);
                    check.EmployeeName = string.Join(",", employees.Select(e => e.Name));
                }
                check.Address = roomDao.FindById(fb.RoomId).Address;
                checks.Add(check);
            }
            result.ResultList = checks;

            return result;
        }

        public SearchResult SearchHostOrders(int hostId, DateTime? dateFrom, DateTime? dateTo, int start, int length, string search, int status)
        {
            SearchResult result = new SearchResult();
            IQueryable<Order> list = orderDao.SearchByHost(hostId, dateFrom, dateTo, search, status).OrderByDescending(l => l.StartDate);
            result.TotalCount = list.Count();
            list = list.Skip(start).Take(length);

            List<object> checks = new List<object>();
            foreach (Order fb in list)
            {
                OrderCheck check = new OrderCheck(fb);
                if (fb.EmploymentId != null)
                {
                    List<Employment> employees = new EmployeeController().FindByIDS(fb.EmploymentId);
                    check.EmployeeName = string.Join(",", employees.Select(ee => ee.Name));
                }
                check.Address = roomDao.FindById(fb.RoomId).Address;
                checks.Add(check);
            }
            result.ResultList = checks;

            return result;
        }

        public SearchResult SearchEmployeeOrders(int employeeID, DateTime? dateFrom, DateTime? dateTo, int start, int length, string search, int status)
        {
            SearchResult result = new SearchResult();
            IQueryable<Order> list = orderDao.SearchByEmployee(employeeID, dateFrom, dateTo, search, status).OrderByDescending(l => l.StartDate);
            result.TotalCount = list.Count();
            list = list.Skip(start).Take(length);

            List<object> checks = new List<object>();
            foreach (Order fb in list)
            {
                OrderCheck check = new OrderCheck(fb);
                if (fb.EmploymentId != null)
                {
                    List<Employment> employees = new EmployeeController().FindByIDS(fb.EmploymentId);
                    check.EmployeeName = string.Join(",", employees.Select(ee => ee.Name));
                }
                checks.Add(check);
            }
            result.ResultList = checks;

            return result;
        }

        public bool DeleteOrder(int id)
        {
            Order item = orderDao.FindById(id);
            if (item == null) return false;

            return orderDao.Delete(id);
        }

        public Order FindBy(int id)
        {
            return orderDao.FindById(id);
        }

        public List<ServiceAllocCheck> FindServiceByOrder(int id)
        {
            List<OrderServiceAlloc> serviceList = allocDao.FindByOrder(id).ToList();
            List<ServiceAllocCheck> result = new List<ServiceAllocCheck>();
            foreach(OrderServiceAlloc service in serviceList)
            {
                result.Add(new ServiceAllocCheck(service));
            }
            return result;
        }

        public int? AddOrder(int status, DateTime dateFrom, DateTime dateTo, int numberOfGuests, double totalAmount, int? hostId, int? roomId, DateTime dateCreated, string note, int? paymentID, int? voucherID)
        {
            Order addOrder = new Order();
            addOrder.Status = status;
            addOrder.StartDate = dateFrom;
            addOrder.EndDate = dateTo;
            addOrder.NumberOfGuests = numberOfGuests;
            addOrder.note = note;
            addOrder.TotalAmount = totalAmount;
            addOrder.DateCreated = dateCreated;
            if (hostId == null || roomId == null) return null;
            addOrder.HostId = hostId ?? 0;
            addOrder.RoomId = roomId ?? 0;
            if (paymentID != null) addOrder.PaymentId = paymentID;
            if (voucherID != null) addOrder.VoucherId = voucherID;

            int? result = orderDao.Insert2(addOrder);
            if (result != null && paymentID != null)
            {
                PaymentDAO paymentDAO = new PaymentDAO();
                Payment payment = paymentDAO.FindById(paymentID ?? 0);
                payment.OrderId = result;
                paymentDAO.Update(payment);
            }
            return result;
        }

        public bool UpdateOrder(int id, int status, DateTime dateFrom, DateTime dateTo, int numberOfGuests, double totalAmount, string employeeIds, string note, int? paymentID, int? voucherID)
        {
            Order order = orderDao.FindById(id);
            if (order == null) return false;

            order.StartDate = dateFrom;
            order.EndDate = dateTo;
            order.Status = status;
            order.NumberOfGuests = numberOfGuests;
            order.TotalAmount = totalAmount;
            order.note = note;
            if (!string.IsNullOrEmpty(employeeIds)) order.EmploymentId = employeeIds;
            else order.EmploymentId = null;
            if (paymentID != null)
            {
                order.Payment = new PaymentDAO().FindById(paymentID ?? 0);
            }
            if (voucherID != null)
            {
                order.Voucher = new VoucherDAO().FindById(voucherID ?? 0);
            }

            return orderDao.Update(order);
        }

        public bool AddServiceAlloc(int? id, List<ServiceAllocCheck> serviceList)
        {
            if (id == null) return false;

            List<OrderServiceAlloc> list = new List<OrderServiceAlloc> ();
            foreach (ServiceAllocCheck service in serviceList)
            {
                OrderServiceAlloc orderServiceAlloc = new OrderServiceAlloc ();
                orderServiceAlloc.OrderId = id;
                orderServiceAlloc.ServiceId = service.serviceId;
                orderServiceAlloc.Quantity = service.Quantity;
                orderServiceAlloc.Amount = service.Amount;

                list.Add(orderServiceAlloc);
            }
            allocDao.DeleteByOrder(id ?? 0);
            return allocDao.InsertByOrder(list);
        }
    }
}