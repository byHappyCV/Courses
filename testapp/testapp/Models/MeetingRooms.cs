using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testapp.Models
{
    public class MeetingRooms
    {
        public MeetingRooms()
        {
            this.Reservations = new HashSet<Reservations>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }

        public virtual ICollection<Reservations> Reservations { get; set; }
    }
}