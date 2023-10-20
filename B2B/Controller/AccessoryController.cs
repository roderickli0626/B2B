using B2B.DAO;
using B2B.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B2B.Controller
{
    public class AccessoryController
    {
        AccessoryDAO accessoryDAO;
        public AccessoryController()
        { 
            accessoryDAO = new AccessoryDAO();
        }

        public SearchResult SearchBy(int start, int length, string search)
        {
            SearchResult result = new SearchResult();
            IQueryable<Accessory> list = accessoryDAO.SearchBy(search).OrderBy(l => l.Id);
            result.TotalCount = list.Count();
            list = list.Skip(start).Take(length);

            List<object> checks = new List<object>();
            foreach (Accessory fb in list)
            {
                AccessoryCheck check = new AccessoryCheck(fb);
                checks.Add(check);
            }
            result.ResultList = checks;

            return result;
        }

        public bool DeleteAccessory(int id)
        {
            Accessory item = accessoryDAO.FindById(id);
            if (item == null) return false;

            return accessoryDAO.Delete(id);
        }

        public Accessory FindBy(int id)
        {
            return accessoryDAO.FindById(id);
        }

        public bool SaveAccessory(int id, string description, string image)
        {
            if (id == 0)
            {
                Accessory accessory = new Accessory();
                accessory.Description = description;
                accessory.Image = image;

                return accessoryDAO.Insert(accessory);
            }
            else
            {
                Accessory accessory = accessoryDAO.FindById(id);
                accessory.Description = description;
                accessory.Image = image;

                return accessoryDAO.Update(accessory);
            }
        }

    }
}