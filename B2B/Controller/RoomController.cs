using B2B.DAO;
using B2B.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B2B.Controller
{
    public class RoomController
    {
        public RoomDAO roomDao;
        public RoomAccessoryAllocDAO roomAccessoryAllocDao;
        public AccommodationDAO accommodationDao;

        public RoomController()
        {
            roomDao = new RoomDAO();
            roomAccessoryAllocDao = new RoomAccessoryAllocDAO();
            accommodationDao = new AccommodationDAO();
        }

        public SearchResult SearchRooms(int lift, int type, int start, int length, string search, int status)
        {
            SearchResult result = new SearchResult();
            IQueryable<Room> list = roomDao.SearchBy(lift, type, search, status).OrderBy(l => l.Id);
            result.TotalCount = list.Count();
            list = list.Skip(start).Take(length);

            List<object> checks = new List<object>();
            foreach (Room fb in list)
            {
                RoomCheck check = new RoomCheck(fb);
                checks.Add(check);
            }
            result.ResultList = checks;

            return result;
        }

        public SearchResult SearchHostRooms(int hostId, int lift, int type, int start, int length, string search, int status)
        {
            SearchResult result = new SearchResult();
            IQueryable<Room> list = roomDao.SearchByHost(hostId, lift, type, search, status).OrderBy(l => l.Id);
            result.TotalCount = list.Count();
            list = list.Skip(start).Take(length);

            List<object> checks = new List<object>();
            foreach (Room fb in list)
            {
                RoomCheck check = new RoomCheck(fb);
                checks.Add(check);
            }
            result.ResultList = checks;

            return result;
        }

        public bool DeleteRoom(int id)
        {
            Room item = roomDao.FindById(id);
            if (item == null) return false;

            return roomDao.Delete(id);
        }

        public Room FindBy(int id)
        {
            return roomDao.FindById(id);
        }

        public List<AccessoryAllocCheck> FindAccessoriesByRoom(int id)
        {
            List<AccessoryAllocCheck> resultList = new List<AccessoryAllocCheck>();
            List<RoomAccessoryAlloc> alloList = roomAccessoryAllocDao.FindByRoom(id).ToList();
            foreach (RoomAccessoryAlloc roomAccessoryAlloc in alloList)
            {
                resultList.Add(new AccessoryAllocCheck(roomAccessoryAlloc));
            }
            return resultList;
        }

        public int? AddRoom(int status, int? typeId, int? lift, string address, string stairCases, string floor, int? hostId, string note, int? priceGroupId)
        {
            if (hostId == null || typeId == null)
            {
                return null;
            }
            Room room = new Room();
            room.Status = status;
            room.TypeId = typeId;
            room.StairCases = stairCases;
            room.Floor = floor;
            room.Note = note;
            room.HostId = hostId;
            room.PriceListGroupId = priceGroupId;
            room.Address = address;
            room.Lift = lift == 1 ? true : false;
            
            return roomDao.Insert2(room);
        }

        public bool AddAccessoryAlloc(int? id, List<AccessoryAllocCheck> accessoryList)
        {
            if (id == null) return false;

            List<RoomAccessoryAlloc> list = new List<RoomAccessoryAlloc>();
            foreach (AccessoryAllocCheck accessory in accessoryList)
            {
                RoomAccessoryAlloc roomAccessoryAlloc = new RoomAccessoryAlloc();
                roomAccessoryAlloc.RoomId = id;
                roomAccessoryAlloc.AccessoryId = accessory.AccessoryId;
                roomAccessoryAlloc.Quantity = accessory.Quantity;

                list.Add(roomAccessoryAlloc);
            }
            roomAccessoryAllocDao.DeleteByRoom(id ?? 0);
            return roomAccessoryAllocDao.InsertByRoom(list);
        }

        public bool UpdateRoom(int id, int status, int? typeId, int? lift, string address, string stairCases, string floor, string note, int? priceGroupId)
        {
            Room room = roomDao.FindById(id);
            if (room == null) return false;
            Accomodation accomodation = new Accomodation();
            accomodation = accommodationDao.FindById(typeId ?? 0);
            room.Status = status;
            //room.TypeId = typeId;
            room.Accomodation = accomodation;
            room.StairCases = stairCases;
            room.Floor = floor;
            room.Note = note;
            room.Address = address;
            room.Lift = lift == 1 ? true : false;
            room.PriceListGroupId = priceGroupId;

            return roomDao.Update(room);
        }
    }
}