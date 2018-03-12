using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HotelsApp.Context;
using HotelsApp.Models;
using HotelsApp.Repository;

namespace HotelsApp.Controllers
{
    public class HomeController : Controller
    {
        ReservationRepository rep = new ReservationRepository();
        public async Task<ActionResult> Index()
        {
            return View(await rep.GetMeetingRoomsAsync());
        }

        public async Task<ActionResult> ShowInfo(int? id)
        { 

            if(id == null)
            {
                return RedirectToAction("Index");
            }
            var res = await rep.GetReservationsByIdAsync((int)id);
            if (!res.Any())
            {
                return View(new List<Reservation> {new Reservation() {RoomId = (int) id}});
            }
            return View(await rep.GetReservationsByIdAsync((int)id));
        }
        public async Task<ActionResult> Reservation(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            return View(await rep.GetMeetingRoomByIdAsync((int)id));
        }
        [HttpPost]
        public async Task<ActionResult> AddReservation(int? id, TimeSpan start, TimeSpan end)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Reservation res = await rep.DateCheckAsyc(start, end, (int)id);
            if (res != null)
            {
                await rep.AddReservationAsync(res);
            }
            else
            {
                return RedirectToAction("Reservation", new {id = id});
            }
            return RedirectToAction("ShowInfo", new {id = id});
        }

        public async Task<ActionResult> DeleteReservation(int? resId)
        {
            if (resId != null)
            {
                var id = await rep.GetReservationByIdAsync((int) resId);
                await rep.DeleteReservationAsync((int) resId);
                return RedirectToAction("ShowInfo", new {id = id.RoomId});
            }
            return RedirectToAction("Index");
        }
    }
}