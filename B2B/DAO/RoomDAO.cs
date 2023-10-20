using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace B2B.DAO
{
    public class RoomDAO : BasicDAO
    {
        public RoomDAO() { }

        public List<Room> FindAll()
        {
            Table<Room> table = GetContext().Rooms;
            return table.ToList();
        }

        public List<Room> FindByOwner(int ownerId)
        {
            Table<Room> table = GetContext().Rooms;
            return table.Where(
                u =>
                u.HostId == ownerId && u.Status == 2).ToList();
        }

        public Room FindById(int id)
        {
            Table<Room> table = GetContext().Rooms;
            return table.Where(
                u =>
                u.Id == id).FirstOrDefault();
        }

        public bool Insert(Room room)
        {
            try
            {
                GetContext().Rooms.InsertOnSubmit(room);
                GetContext().SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }
        public int? Insert2(Room room)
        {
            try
            {
                GetContext().Rooms.InsertOnSubmit(room);
                GetContext().SubmitChanges();
                return room.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public bool Update(Room room)
        {
            GetContext().SubmitChanges();
            GetContext().Refresh(RefreshMode.OverwriteCurrentValues, room);
            return true;
        }
        public bool Delete(int id)
        {
            Room room = GetContext().Rooms.SingleOrDefault(u => u.Id == id);
            GetContext().Rooms.DeleteOnSubmit(room);
            GetContext().SubmitChanges();
            return true;
        }

        public IQueryable<Room> SearchBy(int lift, int type, string search, int status)
        {
            Table<Room> table = GetContext().Rooms;

            IQueryable<Room> result = table.Where(
                u =>
                u.Host.Name.Contains(search) || u.Address.Contains(search)
            );
            if (status != 0)
                result = result.Where(d => d.Status == status);

            if (lift != 0)
                result = result.Where(u => u.Lift == (lift == 1));

            if (type != 0)
                result = result.Where(u => u.TypeId == type);

            return result;
        }

        public IQueryable<Room> SearchByHost(int hostId, int lift, int type, string search, int status)
        {
            IQueryable<Room> result = GetContext().Rooms.Where(r => r.HostId == hostId);
            result = result.Where(
                u => u.Address.Contains(search)
            );
            if (status != 0)
                result = result.Where(d => d.Status == status);

            if (lift != 0)
                result = result.Where(u => u.Lift == (lift == 1));

            if (type != 0)
                result = result.Where(u => u.TypeId == type);

            return result;
        }
    }
}