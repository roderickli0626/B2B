using B2B.DAO;
using B2B.Model;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B2B.Controller
{
    public class EmployeeController
    {
        EmploymentDAO employmentDAO = null;

        public EmployeeController()
        {
            employmentDAO= new EmploymentDAO();
        }

        public SearchResult SearchBy(int start, int length, string search, int searchKey)
        {
            SearchResult result = new SearchResult();
            IQueryable<Employment> list = employmentDAO.SearchBy(search, searchKey).OrderBy(l => l.Id);
            result.TotalCount = list.Count();
            list = list.Skip(start).Take(length);

            List<object> checks = new List<object>();
            foreach (Employment fb in list)
            {
                EmployeeCheck check = new EmployeeCheck(fb);
                checks.Add(check);
            }
            result.ResultList = checks;

            return result;
        }

        public bool DeleteEmployee(int id)
        {
            Employment item = employmentDAO.FindById(id);
            if (item == null) return false;

            return employmentDAO.Delete(id);
        }

        public Employment FindBy(int id)
        {
            return employmentDAO.FindById(id);
        }
        public List<Employment> FindByIDS(string Ids)
        {
            if (Ids == null) return null;
            List<int> intList = Ids.Split(',').Select(int.Parse).ToList();
            List<Employment> result = new List<Employment>();
            foreach (int i in intList)
            {
                result.Add(FindBy(i));
            }
            return result;
        }

        public Employment FindByNamePassword(string name, string password)
        {
            return employmentDAO.FindByNamePassword(name, password);
        }

        public bool SaveEmployee(int id, string name, string surname, string mobile, string note, string password)
        {
            if (id == 0)
            {
                Employment employment = new Employment();
                employment.Name = name;
                employment.Surname = surname;
                employment.MobilePhone = mobile;
                employment.Note = note;
                employment.Password = password;

                return employmentDAO.Insert(employment);
            }
            else
            {
                Employment employment = employmentDAO.FindById(id);
                employment.Name = name;
                employment.Surname= surname;
                employment.MobilePhone= mobile;
                employment.Note = note;
                employment.Password= password;

                return employmentDAO.Update(employment);
            }
        }
    }
}