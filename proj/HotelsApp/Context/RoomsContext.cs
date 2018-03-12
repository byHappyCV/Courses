using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using HotelsApp.Models;

namespace HotelsApp.Context
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