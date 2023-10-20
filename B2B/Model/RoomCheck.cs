using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B2B.Model
{
    public class RoomCheck
    {
        private Room room = null;

        public RoomCheck(Room room)
        {
            this.room = room;
        }
        public int Id
        {
            get
            {
                return room == null ? 0 : room.Id;
            }
        }

        public string Owner
        {
            get
            {
                return room == null ? "" : room.Host.Name;
            }
        }

        public string Type
        {
            get
            {
                return room == null ? "" : room.Accomodation.Description;
            }
        }

        public string Address
        {
            get
            {
                return room == null ? "" : room.Address;
            }
        }

        public string StairCases
        {
            get
            {
                return room == null ? "" : room.StairCases;
            }
        }

        public string Floor
        {
            get
            {
                return room == null ? "" : room.Floor;
            }
        }

        public bool Lift
        {
            get
            {
                return room == null ? false : room.Lift ?? false;
            }
        }

        public int Status
        {
            get
            {
                return room?.Status ?? 0;
            }
        }

        public string PriceListGroup
        {
            get
            {
                return room == null ? "" : room.PriceListGroup?.DescriptionShort;
            }
        }
    }
}