using B2B.DAO;
using B2B.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B2B.Controller
{
    public class PriceListGroupController
    {
        private PriceListGroupDAO priceListGroupDAO;

        public PriceListGroupController() 
        {
            priceListGroupDAO = new PriceListGroupDAO();
        }

        public SearchResult SearchBy(int start, int length, string search)
        {
            SearchResult result = new SearchResult();
            IQueryable<PriceListGroup> list = priceListGroupDAO.SearchBy(search).OrderBy(l => l.Id);
            result.TotalCount = list.Count();
            list = list.Skip(start).Take(length);

            List<object> checks = new List<object>();
            foreach (PriceListGroup fb in list)
            {
                PriceListGroupCheck check = new PriceListGroupCheck(fb);
                checks.Add(check);
            }
            result.ResultList = checks;

            return result;
        }

        public bool DeletePriceListGroup(int id)
        {
            PriceListGroup item = priceListGroupDAO.FindById(id);
            if (item == null) return false;

            return priceListGroupDAO.Delete(id);
        }

        public PriceListGroup FindBy(int id)
        {
            return priceListGroupDAO.FindById(id);
        }

        public bool SavePriceListGroup(int id, string description, double percent)
        {
            if (id == 0)
            {
                PriceListGroup priceListGroup = new PriceListGroup();
                priceListGroup.DescriptionShort = description;
                priceListGroup.Percentuale = percent;

                return priceListGroupDAO.Insert(priceListGroup);
            }
            else
            {
                PriceListGroup priceListGroup = priceListGroupDAO.FindById(id);
                priceListGroup.DescriptionShort = description;
                priceListGroup.Percentuale = percent;

                return priceListGroupDAO.Update(priceListGroup);
            }
        }

    }
}