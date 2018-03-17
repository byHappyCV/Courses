using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using testapp.Common;
using testapp.Models;

namespace testapp.Repository
{
    public class ReservationRepository
    {
       
        public async Task<List<Reservation>> GetReservAsync(MeetingRoom room)
        {
            List<Reservation> res = null;
            using (RoomsContext dbContext = new RoomsContext())
            {
                var r = await dbContext.MeetingRooms.ToListAsync();
                res = r.Find(c => c.Id == room.Id).Reservations.ToList();
                res.Sort();
            }
            return res;
        }
        public async Task<bool> AddReservationAsync(ReservationViewModel model)
        {
            Time time = new Time(model.Start, model.End);
            using (RoomsContext dbContext = new RoomsContext())
            {
                var room = dbContext.MeetingRooms.ToList().Find(c => c.Id == model.RoomId);
                Reservation res = new Reservation
                {
                    StartTime = time.Start,
                    EndTime = time.End,
                    RoomId = room.Id,
                    Room = room,
                    UserName = model.UserName,
                    UserStringId = model.UserStringId
                };
                if (await time.CheckReservation(room, res))
                {
                    room.Reservations.Add(res);
                    await dbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
        }

        public async Task<IEnumerable<MeetingRoom>> GetMeetingRoomsAsync()
        {
            IEnumerable<MeetingRoom> result = null;
            using (RoomsContext dbContext = new RoomsContext())
            {
                result = await dbContext.MeetingRooms.ToListAsync();
            }
            return result;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByIdAsync(int id)
        {
            List<Reservation> res = null;
            using (RoomsContext dbContext = new RoomsContext())
            {
                var room = await dbContext.MeetingRooms.FirstOrDefaultAsync(c => c.Id == id);
                if (room != null) res = room.Reservations.ToList();
                res?.Sort();
            }
            return res;
        }

        public async Task<MeetingRoom> GetMeetingRoomByIdAsync(int id)
        {
            MeetingRoom res = null;
            using (RoomsContext dbContext = new RoomsContext())
            {
                res = await dbContext.MeetingRooms.FirstOrDefaultAsync(c => c.Id == id);

            }
            return res;
        }

        public async Task<Reservation> GetReservationByIdAsync(int id)
        {
            Reservation result = null;
            using (RoomsContext dbContext = new RoomsContext())
            {
                result = await dbContext.Reservations.FirstOrDefaultAsync(c => c.Id == id);
            }
            return result;
        }

        public async Task DeleteReservationAsync(int id)
        {
            using (RoomsContext dbContext = new RoomsContext())
            {
                var res = await dbContext.Reservations.FirstOrDefaultAsync(c => c.Id == id);
                if (res != null) dbContext.Reservations.Remove(res);
                await dbContext.SaveChangesAsync();
            }
        }

    }
}