using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace test
{
    public class RoomsContext : DbContext
    {
        public RoomsContext() : base("MeetingRoomsContext")
        {

        }
        public virtual DbSet<MeetingRooms> MeetingRooms { get; set; }
        public virtual DbSet<Reservations> Reservations { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}