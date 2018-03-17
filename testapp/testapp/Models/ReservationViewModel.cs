using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testapp.Models
{
    public class ReservationViewModel
    {
        public int? RoomId { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public string UserStringId { get; set; }
        public string UserName { get; set; }
    }
}