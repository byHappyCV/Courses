using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using testapp.Common;
using testapp.Models;
using testapp.Repository;

namespace testapp.Controllers
{
    public class HomeController : Controller
    {
        ReservationRepository rep = new ReservationRepository();

        [Authorize]
        public void AddUser()
        {
            using (RoomsContext dbContext = new RoomsContext())
            {
                if (dbContext.Users.ToList().Any(c => c.UserId == User.Identity.GetUserId()))
                {

                }
                else
                {
                    dbContext.Users.Add(new Users {UserId = User.Identity.GetUserId(), UserName = User.Identity.Name});
                    dbContext.SaveChanges();
                }
                
            }
        }
        public ActionResult Index()
        {
               AddUser();

            return View();
        }

        
        public async Task<ActionResult> ShowRooms()
        {
            return View(await rep.GetMeetingRoomsAsync());
        }

        public ActionResult ShowInfo(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ShowInfo");
            }
            ShowInfoViewModel viewModel = new ShowInfoViewModel
            {
                List = rep.GetReservationsByIdAsync((int)id),
                RoomId = (int)id,
            };
            return View(viewModel);
        }
        [Authorize]
        public async Task<ActionResult> Reservation(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ShowInfo");
            }
            ReservationViewModel viewModel = new ReservationViewModel
            {
                RoomId = (int)id,
                User = await rep.GetUserAsync(User),
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> AddReservation(ReservationViewModel viewModel)
        {
            if (viewModel.RoomId == null)
            {
                return RedirectToAction("ShowInfo");
            }
            viewModel.Time = new Time(viewModel.Start, viewModel.End);
            if (ModelState.IsValid)
            {
                
            }
            viewModel.User = await rep.GetUserAsync(User);
            string res;
            if (new Time(viewModel.Start, viewModel.End).Check())
            {
                res = await rep.AddReservationAsync(viewModel);
            }
            else
            {
                ModelState.AddModelError("Add","incorrect time");
                return RedirectToAction("Reservation", new { id = viewModel.RoomId});
            }
            return RedirectToAction("ShowInfo", new { id = viewModel.RoomId});
        }

        [Authorize]
        public async Task<ActionResult> DeleteReservation(int? resId)
        {
            if (resId != null)
            {
                var id = await rep.GetReservationByIdAsync((int)resId);
                await rep.DeleteReservationAsync((int)resId, User);
                return RedirectToAction("ShowInfo", new { id = id.RoomId});
            }
            return RedirectToAction("ShowInfo");
        }
    }
}