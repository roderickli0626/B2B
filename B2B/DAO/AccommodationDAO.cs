using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace B2B.DAO
{
    public class AccommodationDAO : BasicDAO
    {
        public AccommodationDAO() { }

        public List<Accomodation> FindAll()
        {
            Table<Accomodation> table = GetContext().Accomodations;
            return table.ToList();
        }

        public Accomodation FindById(int id)
        {
            Table<Accomodation> table = GetContext().Accomodations;
            return table.Where(
                u =>
                u.Id == id).FirstOrDefault();
        }


        public bool Insert(Accomodation accommodation)
        {
            try
            {
                GetContext().Accomodations.InsertOnSubmit(accommodation);
                GetContext().SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }

        public bool Update(Accomodation accommodation)
        {
            GetContext().SubmitChanges();
            GetContext().Refresh(RefreshMode.OverwriteCurrentValues, accommodation);
            return true;
        }
        public bool Delete(int id)
        {
            Accomodation accommodation = GetContext().Accomodations.SingleOrDefault(u => u.Id == id);
            GetContext().Accomodations.DeleteOnSubmit(accommodation);
            GetContext().SubmitChanges();
            return true;
        }

        public IQueryable<Accomodation> SearchBy(string search)
        {
            Table<Accomodation> table = GetContext().Accomodations;

            IQueryable<Accomodation> result = table.Where(u => u.Description.Contains(search));
            return result;
        }
    }
}