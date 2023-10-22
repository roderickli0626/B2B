using B2B.DAO;
using B2B.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B2B.Controller
{
    public class GrandServiceController
    {
        private GrandServiceDAO grandServiceDao;
        public GrandServiceController() 
        { 
            grandServiceDao = new GrandServiceDAO();
        }

        public SearchResult SearchBy(int start, int length, string search)
        {
            SearchResult result = new SearchResult();
            IQueryable<GrandService> list = grandServiceDao.SearchBy(search).OrderBy(l => l.Id);
            result.TotalCount = list.Count();
            list = list.Skip(start).Take(length);

            List<object> checks = new List<object>();
            foreach (GrandService fb in list)
            {
                GrandServiceCheck check = new GrandServiceCheck(fb);
                checks.Add(check);
            }
            result.ResultList = checks;

            return result;
        }

        public bool DeleteGrandService(int id)
        {
            GrandService item = grandServiceDao.FindById(id);
            if (item == null) return false;

            return grandServiceDao.Delete(id);
        }

        public GrandService FindBy(int id)
        {
            return grandServiceDao.FindById(id);
        }

        public bool SaveGrandService(int id, string title, string description)
        {
            if (id == 0)
            {
                GrandService grandService = new GrandService();
                grandService.Title = title;
                grandService.Description = description;

                return grandServiceDao.Insert(grandService);
            }
            else
            {
                GrandService grandService = grandServiceDao.FindById(id);
                grandService.Title = title;
                grandService.Description = description;

                return grandServiceDao.Update(grandService);
            }
        }

    }
}