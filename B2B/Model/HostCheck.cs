using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B2B.Model
{
    public class HostCheck
    {
        private Host host = null;

        public HostCheck(Host host)
        {
            this.host = host;
        }
        public int Id
        {
            get
            {
                return host == null ? 0 : host.Id;
            }
        }

        public string Name
        {
            get
            {
                return host == null ? "" : host.Name;
            }
        }

        public string Surname
        {
            get
            {
                return host == null ? "" : host.Surname;
            }
        }

        public string Mobile
        {
            get
            {
                return host == null ? "" : host.Mobile;
            }
        }

        public string Phone
        {
            get
            {
                return host == null ? "" : host.Phone;
            }
        }

        public string Email
        {
            get
            {
                return host == null ? "" : host.Email;
            }
        }

        public string Note
        {
            get
            {
                return host ==  null? "" : host.Note;
            }
        }

    }
}