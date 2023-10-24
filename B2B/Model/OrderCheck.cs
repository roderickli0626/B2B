using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B2B.Model
{
    public class OrderCheck
    {
        private Order order = null;
        public OrderCheck(Order order) 
        { 
            this.order = order;
        }

        public int Id
        {
            get
            {
                return order == null ? 0 : order.Id;
            }
        }

        public string Owner
        {
            get
            {
                return order == null ? "" : order.Host.Name;
            }
        }
        public string StartDate
        {
            get
            {
                return order == null ? "" : order.StartDate?.ToString("dd/MM/yyyy");
            }
        }

        public string EndDate
        {
            get
            {
                return order == null ? "" : order.EndDate?.ToString("dd/MM/yyyy");
            }
        }

        public int NumberOfGuests
        {
            get
            {
                return order == null ? 0 : (order.NumberOfGuests ?? 0);
            }
        }

        public double TotalAmount
        {
            get
            {
                return order == null ? 0 : (order.TotalAmount ?? 0);
            }
        }
        public int Status
        {
            get
            {
                return order?.Status ?? 0;
            }
        }

        public string EmployeeName
        {
            get; set;
        }

        public string Address
        {
            get; set;
        }
    }
}