using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HotelsApp.Common;
using HotelsApp.Context;
using HotelsApp.Models;

namespace HotelsApp.Repository
{
    public class ReservationRepository
    {

        public async Task AddReservationAsync(Reservation res)
        {
            using (RoomsContext dbContext = new RoomsContext())
            {
                var room = dbContext.MeetingRooms.ToList().Find(c => c.Id == res.RoomId);
                room.Reservations.Add(new Reservation()
                {
                    StartTime = res.StartTime,
                    EndTime = res.EndTime,
                    RoomId = res.RoomId,
                    Room = res.Room}
                );
                await dbContext.SaveChangesAsync();
            }
        }
        public async Task<Reservation> DateCheckAsyc(TimeSpan start, TimeSpan end, int id)
        {
            Time time = new Time(start, end);
            using (RoomsContext dbContext = new RoomsContext())
            {
                var room = dbContext.MeetingRooms.ToList().Find(c => c.Id == id);
                var res = await GetReservationsByIdAsync(id);
                var list = res.ToList();
                for (int i = 0; i <= list.Count; i++)
                {
                    if (!time.Check())
                    {
                        return null;
                    }
                    if (list.Count >= 2)
                    {
                        if (i == list.Count - 1)
                        {
                            break;
                        }
                        if (time.Check(new Time(list[i].StartTime, list[i].EndTime),
                            new Time(list[i + 1].StartTime, list[i + 1].EndTime)))
                        {
                            return new Reservation
                            {
                                StartTime = time.Start,
                                EndTime = time.End,
                                RoomId = room.Id,
                                Room = room
                            };
                        }
                        if (time.Start >= list.Last().EndTime)
                        {
                            return new Reservation
                            {
                                StartTime = time.Start,
                                EndTime = time.End,
                                RoomId = room.Id,
                                Room = room
                            };
                        }
                        if (time.End > list[i + 1].StartTime)
                        {
                            continue;
                        }
                        if (time.End < list[i + 1].StartTime)
                        {
                            return new Reservation
                            {
                                StartTime = time.Start,
                                EndTime = time.End,
                                RoomId = room.Id,
                                Room = room
                            };
                        }
                        continue;
                    }
                    if (list.Count == 0)
                    {
                        return new Reservation
                        {
                            StartTime = time.Start,
                            EndTime = time.End,
                            RoomId = room.Id,
                            Room = room
                        };
                    }
                    if (time.Check(new Time(list[i].StartTime, list[i].EndTime)))
                    {
                        return new Reservation
                        {
                            StartTime = time.Start,
                            EndTime = time.End,
                            RoomId = room.Id,
                            Room = room
                        };
                    }
                    if (list.Count == 1 && time.Check(new Time(list[0].StartTime, list[0].EndTime)))
                    {
                        return new Reservation
                        {
                            StartTime = time.Start,
                            EndTime = time.End,
                            RoomId = room.Id,
                            Room = room
                        };
                    }


                }
            }
            return null;
        }

        public async Task AddAsync(Reservation res)
        {
            using (RoomsContext dbContext = new RoomsContext())
            {
                var list = dbContext.MeetingRooms.First(c => c.Id == res.RoomId);
                list.Reservations.Add(res);
                await dbContext.SaveChangesAsync();
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