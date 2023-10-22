using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B2B.Model
{
    public class GrandServiceCheck
    {
        private GrandService grandService = null;
        public GrandServiceCheck() { }
        public GrandServiceCheck(GrandService grandService)
        {
            this.grandService = grandService;
            if (grandService == null) return;
            Id = grandService.Id;
            Title = grandService.Title;
            Description = grandService.Description;
        }

        public int Id
        {
            get; set;
        }

        public string Title
        {
            get; set;
        }
        public string Description
        {
            get; set;
        }
    }
}