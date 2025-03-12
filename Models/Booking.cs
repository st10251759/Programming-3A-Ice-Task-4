using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hotel_Booking_Prog_7311_Ice_Task_4.Models
{
    // Booking Model
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [Required]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [Required]
        [Display(Name = "Room")]
        public int RoomId { get; set; }

        [Required]
        [Display(Name = "Check-in Date")]
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }

        [Required]
        [Display(Name = "Check-out Date")]
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }

        [Required]
        [Display(Name = "Total Price")]
        [DataType(DataType.Currency)]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Booking Date")]
        [DataType(DataType.DateTime)]
        public DateTime BookingDate { get; set; }

        [Required]
        [Display(Name = "Status")]
        public string BookingStatus { get; set; }

        // Navigation properties
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }

        // Calculated property for duration
        [NotMapped]
        [Display(Name = "Duration (Nights)")]
        public int Duration => (CheckOutDate - CheckInDate).Days;
    }
}
