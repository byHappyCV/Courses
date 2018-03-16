using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using testapp.Common;
using testapp.Models;
using testapp.Repository;

namespace testapp.Controllers
{
    public class HomeController : Controller
    {
        ReservationRepository rep = new ReservationRepository();

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<ActionResult> ShowRooms()
        {
            return View(await rep.GetMeetingRoomsAsync());
        }

        public async Task<ActionResult> ShowInfo(int? id, string state)
        {
            if (id == null)
            {
                return RedirectToAction("ShowInfo");
            }
            ShowInfoViewModel viewModel = new ShowInfoViewModel
            {
                List = await rep.GetReservationsByIdAsync((int)id),
                RoomId = (int)id,
                State = state,
                UserName = User.Identity.Name
                
            };
            return View(viewModel);
        }
        public ActionResult Reservation(int? id, string state)
        {
            if (id == null)
            {
                return RedirectToAction("ShowInfo");
            }
            ReservationViewModel viewModel = new ReservationViewModel { RoomId = (int)id, State = state, UserName = User.Identity.Name};
            return View(viewModel);
        }
        [HttpPost]
        public async Task<ActionResult> AddReservation(int? id, TimeSpan start, TimeSpan end, string userName)
        {
            if (id == null)
            {
                return RedirectToAction("ShowInfo");
            }
            string res;
            if (new Time(start, end).Check())
            {
                res = await rep.AddReservationAsync(start, end, (int)id, userName);
            }
            else
            {
                return RedirectToAction("Reservation", new { id = id, state = "incorrect date value" });
            }
            return RedirectToAction("ShowInfo", new { id = id, state = res });
        }

        public async Task<ActionResult> DeleteReservation(int? resId)
        {
            if (resId != null)
            {
                var id = await rep.GetReservationByIdAsync((int)resId);
                await rep.DeleteReservationAsync((int)resId);
                return RedirectToAction("ShowInfo", new { id = id.RoomId, state = "deleted" });
            }
            return RedirectToAction("ShowInfo");
        }
    }
}