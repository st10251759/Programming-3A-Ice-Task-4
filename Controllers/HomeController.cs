using Hotel_Booking_Prog_7311_Ice_Task_4.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Hotel_Booking_Prog_7311_Ice_Task_4.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Hotel_Booking_Prog_7311_Ice_Task_4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            var viewModel = new RoomSearchViewModel
            {
                CheckInDate = DateTime.Today,
                CheckOutDate = DateTime.Today.AddDays(1),
                Guests = 1
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SearchRooms(RoomSearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                var availableRooms = _db.Rooms
                    .Where(r => r.IsAvailable && r.MaxOccupancy >= model.Guests)
                    .Where(r => !r.Bookings.Any(b =>
                        (model.CheckInDate >= b.CheckInDate && model.CheckInDate < b.CheckOutDate) ||
                        (model.CheckOutDate > b.CheckInDate && model.CheckOutDate <= b.CheckOutDate) ||
                        (b.CheckInDate >= model.CheckInDate && b.CheckInDate < model.CheckOutDate)))
                    .ToList();

                model.AvailableRooms = availableRooms;
            }
            return View("Index", model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "About Our Hotel";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Information";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}