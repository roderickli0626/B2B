using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B2B.PayPalPayment
{
    public class PaymentDetails
    {
        public string PaymentId = string.Empty;
        List<Parameter> Details = new List<Parameter>();


        public void Set(string key, string value)
        {
            if (Details.Find(p => p.K.ToLower() == key.ToLower()) == null)
            {
                Details.Add(new Parameter(key, value));
            }
            else
            {
                Details.Find(p => p.K.ToLower() == key.ToLower()).V = value;
            }
        }
        public string GetString(string key)
        {
            string value = string.Empty;
            Parameter cd = Details.Find(p => p.K.ToLower() == key.ToLower());
            if ((cd != null) && (!String.IsNullOrEmpty(cd.V)))
                value = cd.V;
            return value;
        }
    }
}