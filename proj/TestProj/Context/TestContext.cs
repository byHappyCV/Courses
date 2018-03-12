using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProj.Models;

namespace TestProj.Context
{
    class TestContext : DbContext
    {
        public TestContext() :base("TestContext")
        {
            
        }

        public DbSet<MeetingRoom> MeetingRooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
