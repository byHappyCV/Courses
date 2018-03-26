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
            RoomsContext dbContext = new RoomsContext();
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
            };
            return View(viewModel);
        }
        public ActionResult Reservation(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ShowInfo");
            }
            ViewBag.Id = (int) id;
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddReservation(ReservationViewModel viewModel)
        {
            if (viewModel.RoomId == null)
            {
                return RedirectToAction("ShowInfo");
            }
            if (new Time(viewModel.Start, viewModel.End).Check())
            {
                if (await rep.AddReservationAsync(viewModel))
                {
                    return RedirectToAction("ShowInfo", new { id = viewModel.RoomId});
                }
                return RedirectToAction("Reservation", new { id = viewModel.RoomId });
            }
            return RedirectToAction("Reservation", new { id = viewModel.RoomId});
            
        }
        [Authorize]
        public async Task<ActionResult> DeleteReservation(int? resId)
        {
            if (resId != null)
            {
                var reservation = await rep.GetReservationByIdAsync((int)resId);
                if (reservation.UserStringId == User.Identity.GetUserId())
                {
                    await rep.DeleteReservationAsync((int)resId);
                    ViewBag.Result = "Deleted";
                    return RedirectToAction("ShowInfo", new { id = reservation.RoomId });
                }
                ViewBag.Error = "You are not an owner of this reservation";
                return RedirectToAction("ShowInfo", new { id = reservation.RoomId });
            }
            return RedirectToAction("ShowInfo");
        }
    }
}