using Hotel_Booking_Prog_7311_Ice_Task_4.Models;
using Hotel_Booking_Prog_7311_Ice_Task_4.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Hotel_Booking_Prog_7311_Ice_Task_4.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CustomersController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Customers
        public ActionResult Index()
        {
            return View(_db.Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var customer = _db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            // Get all bookings for this customer
            var bookings = _db.Bookings
                .Include(b => b.Room)
                .Where(b => b.CustomerId == id)
                .OrderByDescending(b => b.BookingDate)
                .ToList();
            ViewBag.Bookings = bookings;
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _db.Customers.Add(customer);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Log the exception
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                    // For debugging
                    Console.WriteLine(ex.Message);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var customer = _db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _db.Entry(customer).State = EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Log the exception
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                    // For debugging
                    Console.WriteLine(ex.Message);
                }
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var customer = _db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var customer = _db.Customers.Find(id);
                if (customer == null)
                {
                    return NotFound();
                }

                // Check if customer has bookings
                var hasBookings = _db.Bookings.Any(b => b.CustomerId == id);
                if (hasBookings)
                {
                    ModelState.AddModelError("", "Cannot delete customer with existing bookings");
                    return View(customer);
                }

                _db.Customers.Remove(customer);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log the exception
                ModelState.AddModelError("", "Unable to delete customer. Try again, and if the problem persists, see your system administrator.");
                // For debugging
                Console.WriteLine(ex.Message);

                // Refetch the customer to pass back to the view
                var customer = _db.Customers.Find(id);
                return View(customer);
            }
        }
    }
}