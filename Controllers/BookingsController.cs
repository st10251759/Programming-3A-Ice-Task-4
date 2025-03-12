using Hotel_Booking_Prog_7311_Ice_Task_4.Models;
using Hotel_Booking_Prog_7311_Ice_Task_4.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Hotel_Booking_Prog_7311_Ice_Task_4.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookingsController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Bookings
        public ActionResult Index()
        {
            var bookings = _db.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Room)
                .OrderByDescending(b => b.BookingDate)
                .ToList();
            return View(bookings);
        }

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var booking = _db.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Room)
                .FirstOrDefault(b => b.BookingId == id);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/BookingConfirmation/5
        public ActionResult BookingConfirmation(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var booking = _db.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Room)
                .FirstOrDefault(b => b.BookingId == id);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var booking = _db.Bookings.Find(id);
            if (booking == null)
            {
                return NotFound();
            }

            ViewBag.CustomerId = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
                _db.Customers, "CustomerId", "FullName", booking.CustomerId);

            ViewBag.RoomId = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
                _db.Rooms, "RoomId", "RoomNumber", booking.RoomId);

            return View(booking);
        }

        // POST: Bookings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Booking booking)
        {
            if (ModelState.IsValid)
            {
                // Recalculate the total price based on duration and room price
                var room = _db.Rooms.Find(booking.RoomId);
                booking.TotalPrice = room.PricePerNight * (booking.CheckOutDate - booking.CheckInDate).Days;

                _db.Entry(booking).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
                _db.Customers, "CustomerId", "FullName", booking.CustomerId);

            ViewBag.RoomId = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
                _db.Rooms, "RoomId", "RoomNumber", booking.RoomId);

            return View(booking);
        }

        // GET: Bookings/Cancel/5
        public ActionResult Cancel(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var booking = _db.Bookings.Find(id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Cancel/5
        [HttpPost, ActionName("Cancel")]
        [ValidateAntiForgeryToken]
        public ActionResult CancelConfirmed(int id)
        {
            var booking = _db.Bookings.Find(id);
            booking.BookingStatus = "Cancelled";
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
                _db.Customers, "CustomerId", "FullName");

            ViewBag.RoomId = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
                _db.Rooms.Where(r => r.IsAvailable), "RoomId", "RoomNumber");

            return View();
        }

        // POST: Bookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Booking booking)
        {
            if (ModelState.IsValid)
            {
                // Calculate total price
                var room = _db.Rooms.Find(booking.RoomId);
                booking.TotalPrice = room.PricePerNight * (booking.CheckOutDate - booking.CheckInDate).Days;
                booking.BookingDate = DateTime.Now;

                _db.Bookings.Add(booking);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
                _db.Customers, "CustomerId", "FullName", booking.CustomerId);

            ViewBag.RoomId = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
                _db.Rooms.Where(r => r.IsAvailable), "RoomId", "RoomNumber", booking.RoomId);

            return View(booking);
        }
    }
}