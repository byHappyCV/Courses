using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProj.Context;
using TestProj.Models;

namespace TestProj
{
    class ReservRepos
    {
        public void AddReservationAsync(TimeSpan start, TimeSpan end, int id)
        {
            Reservation res = null;
            Time time = new Time(start, end);
            MeetingRoom room = GetMeetingRoomById(id);
            TestContext dbContext = new TestContext();
                var list = dbContext.MeetingRooms.ToList().Find(c => c.Id == id).Reservations.ToList();
                var temp = dbContext.MeetingRooms.ToList().Find(c => c.Id == id).Reservations;
                for (int i = 0; i <= list.Count; i++)
                {
                    if (list.Count >= 2)
                    {
                        if (i == list.Count - 1)
                        {
                            break;
                        }
                        if (time.Check(new Time(list[i].StartTime, list[i].EndTime),
                            new Time(list[i + 1].StartTime, list[i + 1].EndTime)))
                        {
                            temp.Add(new Reservation
                            {
                                StartTime = time.Start,
                                EndTime = time.End,
                                RoomId = id,
                                Room = room
                            });
                            break;
                        }
                        if (time.Start >= list.Last().EndTime)
                        {
                            temp.Add(new Reservation
                            {
                                StartTime = time.Start,
                                EndTime = time.End,
                                RoomId = id,
                                Room = room
                            });
                            break;
                        }
                        if (time.End > list[i + 1].StartTime)
                        {
                            continue;
                        }
                        continue;
                    }
                    if (list.Count == 0)
                    {
                        temp.Add(new Reservation
                        {
                            StartTime = time.Start,
                            EndTime = time.End,
                            RoomId = id,
                            Room = room
                        });
                        break;
                    }
                    if (time.Check(new Time(list[i].StartTime, list[i].EndTime)))
                    {
                        temp.Add(new Reservation
                        {
                            StartTime = time.Start,
                            EndTime = time.End,
                            RoomId = id,
                            Room = room
                        });
                        break;
                    }
                    if (list.Count == 1 && time.Check(new Time(list[0].StartTime, list[0].EndTime)))
                    {
                        temp.Add(new Reservation
                        {
                            StartTime = time.Start,
                            EndTime = time.End,
                            RoomId = id,
                            Room = room
                        });
                        break;
                    }

                
            }
            dbContext.SaveChanges();
        }

        public async Task<Reservation> AddRes(MeetingRoom room, TimeSpan start, TimeSpan end)
        {
            Reservation res = null;
            Time time = new Time(start, end);
            var list = await GetReservAsync(room);
            for (int i = 0; i <= list.Count; i++)
            {
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
            return null;
        }
        public async Task Add(TimeSpan start, TimeSpan end, int id)
        {
            Time time = new Time(start, end);
            using (TestContext dbContext = new TestContext())
            {
                var room = dbContext.MeetingRooms.ToList().Find(c => c.Id == id);
                var list = await GetReservAsync(room);
                for (int i = 0; i <= list.Count; i++)
                {
                    if (list.Count >= 2)
                    {
                        if (i == list.Count - 1)
                        {
                            break;
                        }
                        if (list[0].StartTime >= time.End)
                        {
                            room.Reservations.Add(new Reservation
                            {
                                StartTime = time.Start,
                                EndTime = time.End,
                                RoomId = room.Id,
                                Room = room
                            });
                            break;
                        }
                        if (time.Check(new Time(list[i].StartTime, list[i].EndTime),
                            new Time(list[i + 1].StartTime, list[i + 1].EndTime)))
                        {
                            room.Reservations.Add(new Reservation
                            {
                                StartTime = time.Start,
                                EndTime = time.End,
                                RoomId = room.Id,
                                Room = room
                            });
                            break;
                        }
                        if (time.Start >= list.Last().EndTime)
                        {
                            room.Reservations.Add(new Reservation
                            {
                                StartTime = time.Start,
                                EndTime = time.End,
                                RoomId = room.Id,
                                Room = room
                            });
                            break;
                        }
                        if (time.End > list[i + 1].StartTime)
                        {
                            continue;
                        }
                        continue;
                    }
                    if (list.Count == 0)
                    {
                        room.Reservations.Add(new Reservation
                        {
                            StartTime = time.Start,
                            EndTime = time.End,
                            RoomId = room.Id,
                            Room = room
                        });
                        break;
                    }
                    if (time.Check(new Time(list[i].StartTime, list[i].EndTime)))
                    {
                        room.Reservations.Add(new Reservation
                        {
                            StartTime = time.Start,
                            EndTime = time.End,
                            RoomId = room.Id,
                            Room = room
                        });
                        break;
                    }
                    if (list.Count == 1 && time.Check(new Time(list[0].StartTime, list[0].EndTime)))
                    {
                        room.Reservations.Add(new Reservation
                        {
                            StartTime = time.Start,
                            EndTime = time.End,
                            RoomId = room.Id,
                            Room = room
                        });
                        break;
                    }
                }
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Reservation>> GetReservAsync(MeetingRoom room)
        {
            ICollection<Reservation> res = null;
            using (TestContext dbContext = new TestContext())
            {
                var r = await dbContext.MeetingRooms.ToListAsync();
                res = r.Find(c => c.Id == room.Id).Reservations;
            }
            return res.ToList();
        }

        //public void AddReservation(Time time)
        //{
        //    for (int i = 0; i <= list.Count; i++)
        //    {
        //        if (list.Count == 0)
        //        {
        //            list.Add(time);
        //            break;
        //        }
        //        if (time.Check(list[i]))
        //        {
        //            list.Add(time);
        //            list.Sort();
        //            break;
        //        }
        //        if (list.Count == 1 && time.Check(list[0]))
        //        {
        //            list.Add(time);
        //            list.Sort();
        //            break;
        //        }
        //        if (list.Count >= 2)
        //        {
        //            if (time.Check(list[i], list[i + 1]))
        //            {
        //                list.Add(time);
        //                list.Sort();
        //            }
        //            break;
        //        }
        //    }
        //}
        
        public void Show()
        {
            using (TestContext dbContext = new TestContext())
            {
                foreach (var v in dbContext.MeetingRooms.ToList())
                {
                    Console.WriteLine($"{v.Title} - {v.Location}");
                    foreach (var r in v.Reservations.ToList())
                    {
                        Console.WriteLine($"    {r.StartTime}, {r.EndTime}");
                    }
                }
            }
        }
        public MeetingRoom GetMeetingRoomById(int id)
        {
            MeetingRoom res = null;
            using (TestContext dbContext = new TestContext())
            {
                res = dbContext.MeetingRooms.FirstOrDefault(c => c.Id == id);

            }
            return res;
        }
    }
    class Time : IComparable<Time>
    {
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }

        public Time(TimeSpan start, TimeSpan end)
        {
            Start = start;
            End = end;
        }

        public bool Check(Time time1, Time time3)
        {
            if (time1.End <= Start && End <= time3.Start)
                return true;
            return false;
        }

        public bool Check(Time time1)
        {
            if (time1.End <= Start)
                return true;
            if (time1.Start >= End)
                return true;
            return false;
        }

        public int CompareTo(Time obj)
        {
            return Start.CompareTo(obj.Start);
        }

        
    }
    public class Program
    {
        static void Main(string[] args)
        {
            ReservRepos rep = new ReservRepos();
            //rep.Show();
            Time time = new Time(TimeSpan.Parse("21:00"), TimeSpan.Parse("22:00"));
            //rep.AddReservationAsync(time.Start,time.End,1);
            //rep.Show();
            //rep.AddReservationAsync(time.Start,time.End,1);
            MeetingRoom cv = null;
            rep.Add(time.Start, time.End, 4).Wait();
            rep.Show();

            Console.ReadKey();
        }
    }
}
