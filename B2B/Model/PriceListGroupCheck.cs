using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace B2B.Model
{
    public class PriceListGroupCheck
    {
        private PriceListGroup priceListGroup = null;
        
        public PriceListGroupCheck() { }
        public PriceListGroupCheck(PriceListGroup priceListGroup) 
        { 
            this.priceListGroup = priceListGroup;
            if (priceListGroup == null ) { return; }
            Id = priceListGroup.Id;
            Title = priceListGroup.DescriptionShort;
            Percent = priceListGroup.Percentuale;

        }
        public int Id
        {
            get; set;
        }

        public string Title
        {
            get; set;
        }
        public double? Percent
        {
            get; set;
        }



    }
}