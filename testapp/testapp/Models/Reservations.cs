using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testapp.Common;

namespace testapp.Models
{
    public class Reservations : IComparable<Reservations>
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public System.TimeSpan StartTime { get; set; }
        public System.TimeSpan EndTime { get; set; }
        public int? UserId { get; set; }

        public virtual MeetingRooms Room { get; set; }
        public virtual Users User { get; set; }

        public int CompareTo(Reservations other)
        {
            return new Time(StartTime, EndTime).CompareTo(new Time(other.StartTime, other.EndTime));
        }
    }
}