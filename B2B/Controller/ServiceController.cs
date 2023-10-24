using B2B.DAO;
using B2B.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B2B.Controller
{
    public class ServiceController
    {
        ServiceDAO serviceDAO;
        public ServiceController() 
        { 
            serviceDAO = new ServiceDAO();
        }

        public SearchResult SearchBy(int start, int length, string search, int grandServiceID)
        {
            SearchResult result = new SearchResult();
            IQueryable<Service> list = serviceDAO.SearchBy(search, grandServiceID).OrderBy(l => l.GrandService.Title);
            result.TotalCount = list.Count();
            list = list.Skip(start).Take(length);

            List<object> checks = new List<object>();
            foreach (Service fb in list)
            {
                ServiceCheck check = new ServiceCheck(fb);
                checks.Add(check);
            }
            result.ResultList = checks;

            return result;
        }

        public bool DeleteService(int id)
        {
            Service item = serviceDAO.FindById(id);
            if (item == null) return false;

            return serviceDAO.Delete(id);
        }

        public Service FindBy(int id)
        {
            return serviceDAO.FindById(id);
        }

        public bool SaveService(int id, string descriptionShort, string DescriptionLong, double price, string image, bool hasPriceGroup, int? grandServcieID)
        {
            if (id == 0)
            {
                Service service = new Service();
                service.DescriptionShort = descriptionShort;
                service.DescriptionLong = DescriptionLong;
                service.Price = price;
                service.Image = image;
                service.HavePriceGroup = hasPriceGroup;
                service.GrandServiceID = grandServcieID;

                return serviceDAO.Insert(service);
            }
            else
            {
                Service service = serviceDAO.FindById(id);
                service.DescriptionShort = descriptionShort;
                service.DescriptionLong = DescriptionLong;
                service.Price = price;
                service.Image = image;
                service.HavePriceGroup = hasPriceGroup;
                service.GrandServiceID = grandServcieID;

                return serviceDAO.Update(service);
            }
        }
    }
}