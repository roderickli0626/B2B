using B2B.DAO;
using B2B.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B2B.Controller
{
    public class AccommodationController
    {
        AccommodationDAO accommodationDAO;
        public AccommodationController() 
        {
            accommodationDAO = new AccommodationDAO();
        }

        public SearchResult SearchBy(int start, int length, string search)
        {
            SearchResult result = new SearchResult();
            IQueryable<Accomodation> list = accommodationDAO.SearchBy(search).OrderBy(l => l.Id);
            result.TotalCount = list.Count();
            list = list.Skip(start).Take(length);

            List<object> checks = new List<object>();
            foreach (Accomodation fb in list)
            {
                AccommodationCheck check = new AccommodationCheck(fb);
                checks.Add(check);
            }
            result.ResultList = checks;

            return result;
        }

        public bool DeleteAccommodation(int id)
        {
            Accomodation item = accommodationDAO.FindById(id);
            if (item == null) return false;

            return accommodationDAO.Delete(id);
        }

        public Accomodation FindBy(int id)
        {
            return accommodationDAO.FindById(id);
        }

        public bool SaveAccommodation(int id, string description, string image)
        {
            if (id == 0)
            {
                Accomodation accommodation = new Accomodation();
                accommodation.Description = description;
                accommodation.Image = image;

                return accommodationDAO.Insert(accommodation);
            }
            else
            {
                Accomodation accommodation = accommodationDAO.FindById(id);
                accommodation.Description = description;
                accommodation.Image = image;

                return accommodationDAO.Update(accommodation);
            }
        }
    }
}