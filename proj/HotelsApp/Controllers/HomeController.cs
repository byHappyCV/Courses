using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HotelsApp.Common;
using HotelsApp.Context;
using HotelsApp.Models;
using HotelsApp.Repository;
using HotelsApp.ViewModels;

namespace HotelsApp.Controllers
{
    public class HomeController : Controller
    {
        ReservationRepository rep = new ReservationRepository();
        public async Task<ActionResult> Index()
        {
            return View(await rep.GetMeetingRoomsAsync());
        }

        public async Task<ActionResult> ShowInfo(int? id, string state)
        { 
            if(id == null)
            {
                return RedirectToAction("Index");
            }
            ShowInfoViewModel viewModel = new ShowInfoViewModel
            {
                List = await rep.GetReservationsByIdAsync((int)id),
                RoomId = (int)id,
                State = state
            };
            return View(viewModel);
        }
        public ActionResult Reservation(int? id, string state)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            ReservationViewModel viewModel = new ReservationViewModel { RoomId = (int)id, State = state};
            return View(viewModel);
        }
        [HttpPost]
        public async Task<ActionResult> AddReservation(int? id, TimeSpan start, TimeSpan end)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            string res;
            if (new Time(start,end).Check())
            {
                res = await rep.AddReservationAsync(start,end,(int)id);
            }
            else
            {
                return RedirectToAction("Reservation", new {id = id, state = "incorrect date value"});
            }
            return RedirectToAction("ShowInfo", new {id = id, state = res});
        }

        public async Task<ActionResult> DeleteReservation(int? resId)
        {
            if (resId != null)
            {
                var id = await rep.GetReservationByIdAsync((int) resId);
                await rep.DeleteReservationAsync((int) resId);
                return RedirectToAction("ShowInfo", new {id = id.RoomId, state = "deleted"});
            }
            return RedirectToAction("Index");
        }
    }
}