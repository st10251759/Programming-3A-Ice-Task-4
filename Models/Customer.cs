using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hotel_Booking_Prog_7311_Ice_Task_4.Models
{
    // Customer Model
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        // Calculated property
        [NotMapped]
        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {LastName}";

        // Navigation property
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
