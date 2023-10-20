using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace B2B.DAO
{
    public class EmploymentDAO : BasicDAO
    {
        public EmploymentDAO() { }

        public List<Employment> FindAll()
        {
            Table<Employment> table = GetContext().Employments;
            return table.ToList();
        }

        public Employment FindById(int id)
        {
            Table<Employment> table = GetContext().Employments;
            return table.Where(
                u =>
                u.Id == id).FirstOrDefault();
        }

        public Employment FindByNamePassword(string name, string password)
        {
            Table<Employment> table = GetContext().Employments;
            return table.Where(
                u =>
                u.Name == name && u.Password == password).FirstOrDefault();
        }

        public bool Insert(Employment employment)
        {
            try
            {
                GetContext().Employments.InsertOnSubmit(employment);
                GetContext().SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }

        public bool Update(Employment employment)
        {
            GetContext().SubmitChanges();
            GetContext().Refresh(RefreshMode.OverwriteCurrentValues, employment);
            return true;
        }
        public bool Delete(int id)
        {
            Employment employment = GetContext().Employments.SingleOrDefault(u => u.Id == id);
            GetContext().Employments.DeleteOnSubmit(employment);
            GetContext().SubmitChanges();
            return true;
        }

        public IQueryable<Employment> SearchBy(string search, int searchKey)
        {
            Table<Employment> table = GetContext().Employments;

            IQueryable<Employment> result = null;
            if (searchKey == 1)
                result = table.Where(d => d.Name.Contains(search));

            if (searchKey == 2)
                result = table.Where(u => u.Surname.Contains(search));

            if (searchKey == 3)
                result = table.Where(u => u.MobilePhone.Contains(search));

            return result;
        }
    }
}