using Hotel_Booking_Prog_7311_Ice_Task_4.Models;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Booking_Prog_7311_Ice_Task_4.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }

   
}
