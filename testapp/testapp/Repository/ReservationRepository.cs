using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using testapp.Common;
using testapp.Models;

namespace testapp.Repository
{
    public class ReservationRepository
    {
        
        public async Task<ICollection<Reservations>> GetReservAsync(MeetingRooms room)
        {
            List<Reservations> res = null;
            using (RoomsContext dbContext = new RoomsContext())
            {
                var r = await dbContext.MeetingRooms.ToListAsync();
                res = r.Find(c => c.Id == room.Id).Reservations.ToList();
                res.Sort();
            }
            return res;
        }
        public async Task<string> AddReservationAsync(ReservationViewModel viewModel)
        {
            Time time = new Time(viewModel.Start, viewModel.End);
            using (RoomsContext dbContext = new RoomsContext())
            {
                var room = dbContext.MeetingRooms.ToList().Find(c => c.Id == viewModel.RoomId);
                Reservations res = new Reservations
                {
                    StartTime = time.Start,
                    EndTime = time.End,
                    RoomId = room.Id,
                    Room = room,
                    User = viewModel.User,
                    UserId = viewModel.User.Id

                };
                if (await new Time().CheckReservation(room, res))
                {
                    room.Reservations.Add(res);
                    await dbContext.SaveChangesAsync();
                    return "added";
                }
                return "invalid time value";
            }
        }

        public async Task<Users> GetUserAsync(IPrincipal user)
        {
            Users res = null;
            using (RoomsContext dbContext = new RoomsContext())
            {
                var list = await dbContext.Users.ToListAsync();
                res = list.Find(c => c.UserId == user.Identity.GetUserId());
            }
            return res;
        }

        public async Task<IEnumerable<MeetingRooms>> GetMeetingRoomsAsync()
        {
            IEnumerable<MeetingRooms> result = null;
            using (RoomsContext dbContext = new RoomsContext())
            {
                result = await dbContext.MeetingRooms.ToListAsync();
            }
            return result;
        }

        public IEnumerable<Reservations> GetReservationsByIdAsync(int id)
        {
            IEnumerable<Reservations> res = null;
            using (RoomsContext dbContext = new RoomsContext())
            {
                res = dbContext.MeetingRooms.ToList().Find(c => c.Id == 1).Reservations.ToList();
            }
            return res;
        }

        public async Task<MeetingRooms> GetMeetingRoomByIdAsync(int id)
        {
            MeetingRooms res = null;
            using (RoomsContext dbContext = new RoomsContext())
            {
                res = await dbContext.MeetingRooms.FirstOrDefaultAsync(c => c.Id == id);
                return res;
            }
            
        }

        public async Task<Reservations> GetReservationByIdAsync(int id)
        {
            Reservations result = null;
            using (RoomsContext dbContext = new RoomsContext())
            {
                result = await dbContext.Reservations.FirstOrDefaultAsync(c => c.Id == id);
            }
            return result;
        }

        public async Task DeleteReservationAsync(int id, IPrincipal user)
        {
            using (RoomsContext dbContext = new RoomsContext())
            {
                var res = await dbContext.Reservations.FirstOrDefaultAsync(c => c.Id == id);
                if (res != null && res.User.UserId == user.Identity.GetUserId())
                {
                    dbContext.Reservations.Remove(res);
                }
                await dbContext.SaveChangesAsync();
            }
        }

    }
}