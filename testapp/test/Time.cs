using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace test
{
    public class Time : IComparable<Time>
    {
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }

        public Time() { }
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

        public bool Check()
        {
            return End > Start;
        }

        public int CompareTo(Time obj)
        {
            return Start.CompareTo(obj.Start);
        }

        //public async Task<bool> CheckReservation(MeetingRoom room, Reservation res)
        //{
            
        //    var list = await new ReservationRepository().GetReservAsync(room);
        //    Time time = new Time(res.StartTime, res.EndTime);
        //    for (int i = 0; i <= list.Count; i++)
        //    {
        //        if (list.Count >= 2)
        //        {
        //            if (time.Start < list[i].StartTime && time.End > list[i].EndTime)
        //            {
        //                return false;
        //            }
        //            if (time.End < list[i].StartTime)
        //            {
        //                return true;
        //            }
        //            if (time.Check(new Time(list[i].StartTime, list[i].EndTime),
        //                new Time(list[i + 1].StartTime, list[i + 1].EndTime)))
        //            {
        //                return true;
        //            }
        //            if (list.Last().EndTime < time.Start)
        //            {
        //                return true;
        //            }

        //            continue;
        //        }
        //        if (list.Count == 0)
        //        {
        //            return true;
        //        }
        //        if (time.End <= list[0].StartTime)
        //        {
        //            return true;
        //        }
        //        if (time.Start >= list[0].EndTime)
        //        {
        //            return true;
        //        }

        //    }
        //    return false;
        //}


    }
}