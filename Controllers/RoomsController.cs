using Hotel_Booking_Prog_7311_Ice_Task_4.Models;
using Hotel_Booking_Prog_7311_Ice_Task_4.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Hotel_Booking_Prog_7311_Ice_Task_4.Controllers
{
    public class RoomsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public RoomsController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Rooms
        public ActionResult Index()
        {
            return View(_db.Rooms.ToList());
        }

        // GET: Rooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var room = _db.Rooms.Find(id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Rooms/Book/5
        public ActionResult Book(int? id, DateTime? checkIn, DateTime? checkOut)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var room = _db.Rooms.Find(id);
            if (room == null)
            {
                return NotFound();
            }

            var viewModel = new BookingViewModel
            {
                Room = room,
                Booking = new Booking
                {
                    RoomId = room.RoomId,
                    CheckInDate = checkIn ?? DateTime.Today,
                    CheckOutDate = checkOut ?? DateTime.Today.AddDays(1),
                    TotalPrice = room.PricePerNight * (checkOut.HasValue && checkIn.HasValue ?
                        (checkOut.Value - checkIn.Value).Days : 1),
                    BookingStatus = "Pending"
                },
                Customer = new Customer()
            };

            return View(viewModel);
        }

        // POST: Rooms/Book
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Book(BookingViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Check if customer already exists
                var existingCustomer = _db.Customers
                    .FirstOrDefault(c => c.Email == viewModel.Customer.Email);

                if (existingCustomer != null)
                {
                    viewModel.Booking.CustomerId = existingCustomer.CustomerId;
                }
                else
                {
                    _db.Customers.Add(viewModel.Customer);
                    _db.SaveChanges();
                    viewModel.Booking.CustomerId = viewModel.Customer.CustomerId;
                }

                // Calculate total price
                var room = _db.Rooms.Find(viewModel.Booking.RoomId);
                int nights = (viewModel.Booking.CheckOutDate - viewModel.Booking.CheckInDate).Days;
                viewModel.Booking.TotalPrice = room.PricePerNight * nights;
                viewModel.Booking.BookingDate = DateTime.Now;
                viewModel.Booking.BookingStatus = "Confirmed";

                _db.Bookings.Add(viewModel.Booking);
                _db.SaveChanges();

                return RedirectToAction("BookingConfirmation", "Bookings",
                    new { id = viewModel.Booking.BookingId });
            }

            // If ModelState is invalid, refetch the room
            viewModel.Room = _db.Rooms.Find(viewModel.Booking.RoomId);
            return View(viewModel);
        }

        // GET: Rooms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rooms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Room room)
        {
            if (ModelState.IsValid)
            {
                _db.Rooms.Add(room);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(room);
        }

        // GET: Rooms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var room = _db.Rooms.Find(id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Rooms/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Room room)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(room).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(room);
        }

        // GET: Rooms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var room = _db.Rooms.Find(id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var room = _db.Rooms.Find(id);
            _db.Rooms.Remove(room);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}