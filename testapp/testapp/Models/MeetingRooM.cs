using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testapp.Models
{
    public class MeetingRoom
    {
        public MeetingRoom()
        {
            this.Reservations = new HashSet<Reservation>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}