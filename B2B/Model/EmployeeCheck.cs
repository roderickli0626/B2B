using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B2B.Model
{
    public class EmployeeCheck
    {
        private Employment employment;

        public EmployeeCheck(Employment employment)
        {
            this.employment = employment;
        }

        public int Id
        {
            get
            {
                return employment == null ? 0 : employment.Id;
            }
        }

        public string Name
        {
            get
            {
                return employment == null ? "" : employment.Name;
            }
        }

        public string Surname
        {
            get
            {
                return employment == null ? "" : employment.Surname;
            }
        }

        public string Mobile
        {
            get
            {
                return employment == null ? "" : employment.MobilePhone;
            }
        }
    }
}