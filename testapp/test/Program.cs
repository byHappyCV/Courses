using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            //RoomsContext dbContext = new RoomsContext();
            //var list = dbContext.MeetingRooms.ToList().Find(c => c.Id == 1);
            //foreach (var v in list.Reservations.ToList())
            //{
            //    if(v.User != null)
            //        Console.WriteLine($"{v.User.UserName}, {v.Room.Title}, {v.StartTime}");
            //}
            IEnumerable<Reservations> res = null;
            IEnumerable<MeetingRooms> rooms = null;
            IEnumerable<Users> users = null;
            using (RoomsContext dbEntities = new RoomsContext())
            {
                rooms = dbEntities.MeetingRooms.ToList();
                res = dbEntities.Reservations.ToList();
                users = dbEntities.Users.ToList();
            }
            foreach (var v in res)
            {
                if (v.User != null)
                {
                    Console.WriteLine($"{v.User.UserName},{v.Room.Title}");
                }
            }
            
            Console.ReadKey();
        }
    }
}
