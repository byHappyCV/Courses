using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testapp.Common
{
    public class Time : IComparable<Time>
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

        public bool Check()
        {
            if (End > Start)
                return false;
            return false;
        }

        public int CompareTo(Time obj)
        {
            return Start.CompareTo(obj.Start);
        }


    }
}