using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace testapp.Models
{
    public class ShowInfoViewModel
    {
        public IEnumerable<Reservations> List { get; set; }

        public int RoomId { get; set; }
    }
}