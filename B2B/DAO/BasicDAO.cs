using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace B2B.DAO
{
    public class BasicDAO
    {
        public BasicDAO() 
        { 
        }

        public MappingDataContext GetContext()
        {
            var httpContext = HttpContext.Current;
            if (httpContext != null && httpContext.Items.Contains("DBContext"))
                return (MappingDataContext)httpContext.Items["DBContext"];
            return null;
        }

    }
}