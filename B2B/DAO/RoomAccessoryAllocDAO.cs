using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace B2B.DAO
{
    public class RoomAccessoryAllocDAO : BasicDAO
    {
        public RoomAccessoryAllocDAO() { }

        public bool Insert(RoomAccessoryAlloc roomAccessoryAlloc)
        {
            GetContext().RoomAccessoryAllocs.InsertOnSubmit(roomAccessoryAlloc);
            GetContext().SubmitChanges();
            return true;
        }

        public bool InsertByRoom(List<RoomAccessoryAlloc> roomAccessoryAlloc)
        {
            GetContext().RoomAccessoryAllocs.InsertAllOnSubmit(roomAccessoryAlloc);
            GetContext().SubmitChanges();
            return true;
        }
        public void Delete(RoomAccessoryAlloc roomAccessoryAlloc)
        {
            GetContext().RoomAccessoryAllocs.DeleteOnSubmit(roomAccessoryAlloc);
            GetContext().SubmitChanges();
        }
        public void DeleteByRoom(int roomId)
        {
            IEnumerable<RoomAccessoryAlloc> access = GetContext().RoomAccessoryAllocs.Where(o => o.RoomId == roomId);
            GetContext().RoomAccessoryAllocs.DeleteAllOnSubmit(access);
            GetContext().SubmitChanges();
        }
        public IEnumerable<RoomAccessoryAlloc> FindByRoom(int roomId)
        {
            Table<RoomAccessoryAlloc> table = GetContext().RoomAccessoryAllocs;

            IQueryable<RoomAccessoryAlloc> result;

            result = table.Where(
                    u =>
                    u.RoomId == roomId);
            return result;
        }
    }
}