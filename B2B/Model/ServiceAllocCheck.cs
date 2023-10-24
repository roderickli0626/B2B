using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B2B.Model
{
    public class ServiceAllocCheck
    {
        private OrderServiceAlloc orderServiceAlloc = null;

        public ServiceAllocCheck(OrderServiceAlloc orderServiceAlloc)
        {
            this.orderServiceAlloc = orderServiceAlloc;

            if (orderServiceAlloc == null) return;
            Id = orderServiceAlloc.Id;
            serviceId = (int)orderServiceAlloc.ServiceId;
            Description = orderServiceAlloc.Service.DescriptionLong;
            Image = orderServiceAlloc.Service.Image;
            Price = (double)orderServiceAlloc.Service.Price;
            Quantity = (int)orderServiceAlloc.Quantity;
            Amount = (double)orderServiceAlloc.Amount;
            GrandService = orderServiceAlloc.Service.GrandService.Title;
            Service = orderServiceAlloc.Service.DescriptionShort;
        }

        public ServiceAllocCheck() { }

        public int Id
        {
            get; set;
        }

        public int serviceId
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public string Image
        {
            get; set;
        }

        public double Price
        {
            get; set;
        }

        public int Quantity
        {
            get; set;
        }

        public double Amount
        {
            get; set;
        }

        public string GrandService
        {
            get; set;
        }
        public string Service
        {
            get; set;
        }

    }
}