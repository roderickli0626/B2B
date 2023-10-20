using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace B2B.Model
{
    public class AccessoryAllocCheck
    {
        private RoomAccessoryAlloc roomAccessoryAlloc = null;

        public AccessoryAllocCheck() { }

        public AccessoryAllocCheck(RoomAccessoryAlloc roomAccessoryAlloc)
        {
            this.roomAccessoryAlloc = roomAccessoryAlloc;
            if (roomAccessoryAlloc == null) { return; }
            Id = roomAccessoryAlloc.Id;
            AccessoryId = (int)roomAccessoryAlloc.AccessoryId;
            Description = roomAccessoryAlloc.Accessory.Description;
            Image = roomAccessoryAlloc.Accessory.Image;
            Quantity = roomAccessoryAlloc.Quantity ?? 1;
        }

        public int Id
        {
            get; set;
        }

        public int AccessoryId
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public int Quantity
        {
            get; set;
        }

        public string Image
        {
            get; set;
        }
    }
}