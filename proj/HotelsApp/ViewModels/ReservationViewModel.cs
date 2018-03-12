using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelsApp.Models;

namespace HotelsApp.ViewModels
{
    public class ReservationViewModel
    {
        public int RoomId { get; set; }
        public string State { get; set; }
    }
}