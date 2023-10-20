using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace B2B.Model
{
    public class AccessoryCheck
    {
        private Accessory accessory = null;

        public AccessoryCheck(Accessory accessory)
        {
            this.accessory = accessory;

            if (accessory == null) return;
            Id = accessory.Id;
            Description = accessory.Description;
            Image = accessory.Image;
        }
        public AccessoryCheck() { }
        public int Id
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }
        public string Image
        {
            get; set;
        }
    }
}