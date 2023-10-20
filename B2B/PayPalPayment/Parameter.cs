using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B2B.PayPalPayment
{
    public class Parameter
    {
        public string K = string.Empty;
        public string V = string.Empty;

        public Parameter()
        { }
        public Parameter(string key, string value)
        {
            K = key;
            V = value;
        }
        public Parameter(string key, int value)
        {
            K = key;
            V = value.ToString();
        }
        public Parameter(string key, bool value)
        {
            K = key;
            V = value.ToString();
        }
    }
}