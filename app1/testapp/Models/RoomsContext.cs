using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using testapp.Models;

namespace testapp.Models
{
    public class RoomsContext : DbContext
    {
        public RoomsContext() : base("MeetingRoomsContext")
        {

        }
        public DbSet<MeetingRoom> MeetingRooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}