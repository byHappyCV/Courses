using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testapp.Models
{
    public class ShowInfoViewModel
    {
        private List<Reservation> _list = new List<Reservation>();
        public IEnumerable<Reservation> List
        {
            get {return _list; }
            set { _list = value.ToList(); }
        }

        public int RoomId { get; set; }
        public string State { get; set; }
        public string UserName { get; set; }
    }
}