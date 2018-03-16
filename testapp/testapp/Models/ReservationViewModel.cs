using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Web;
using testapp.Common;

namespace testapp.Models
{
    public class ReservationViewModel
    {
        public int? RoomId { get; set; }
        [Required]
        public TimeSpan Start { get; set; }
        [Required]
        public TimeSpan End { get; set; }
        private Time _time;
        public Time Time
        {
            get { return _time; }
            set
            {
                if (new Time(value.Start, value.End).Check())
                {
                    _time = value;
                }
                else
                {
                    _time = null;
                }
            }
        }


        public Users User { get; set; }
    }
}