﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelsApp.Common;

namespace HotelsApp.Models
{
    public class Reservation : IComparable<Reservation>
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public MeetingRoom Room { get; set; }

        public int CompareTo(Reservation other)
        {
            return new Time(StartTime, EndTime).CompareTo(new Time(other.StartTime, other.EndTime));
        }
    }
}