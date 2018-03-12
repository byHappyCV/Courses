using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelsApp.Models;

namespace HotelsApp.ViewModels
{
    public class ShowInfoViewModel
    {
        private List<Reservation> _list = new List<Reservation>();
        public IEnumerable<Reservation> List
        {
            get => _list;
            set => _list = value.ToList();
        }

        public int RoomId { get; set; }
        public string State { get; set; }
    }
}