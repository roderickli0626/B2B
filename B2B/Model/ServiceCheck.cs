using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B2B.Model
{
    public class ServiceCheck
    {
        private Service service = null;

        public ServiceCheck(Service service)
        {
            this.service = service;
        }

        public int Id
        {
            get
            {
                return service == null ? 0 : service.Id;
            }
        }

        public string DescriptionShort
        {
            get
            {
                return service == null ? "" : service.DescriptionShort;
            }
        }
        public string DescriptionLong
        {
            get
            {
                return service == null ? "" : service.DescriptionLong;
            }
        }

        public string Image
        {
            get
            {
                return service == null ? "" : service.Image;
            }
        }

        public double Price
        {
            get
            {
                return service == null ? 0 : service.Price ?? 0;
            }
        }

        public bool HaveGroupPrice
        {
            get
            {
                return service == null ? false : service.HavePriceGroup ?? false;
            }
        }

        public string GrandService
        {
            get
            {
                return service == null ? "" : service.GrandService.Title;
            }
        }
    }
}