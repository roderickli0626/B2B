using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B2B.Model
{
    public class AccommodationCheck
    {
        private Accomodation accommodation = null;

        public AccommodationCheck(Accomodation accomodation)
        {
            this.accommodation = accomodation;

            if (accommodation == null) return;
            Id = accommodation.Id;
            Description = accommodation.Description;
            Image = accommodation.Image;
        }

        public AccommodationCheck() { }
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