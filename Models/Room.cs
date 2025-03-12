using System.ComponentModel.DataAnnotations;

namespace Hotel_Booking_Prog_7311_Ice_Task_4.Models
{
    // Room Model
    public class Room
    {
        [Key]
        public int RoomId { get; set; }

        [Required]
        [Display(Name = "Room Number")]
        public string RoomNumber { get; set; }

        [Required]
        [Display(Name = "Room Type")]
        public string RoomType { get; set; }

        [Required]
        [Display(Name = "Price Per Night")]
        [DataType(DataType.Currency)]
        public decimal PricePerNight { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Max Occupancy")]
        [Range(1, 10)]
        public int MaxOccupancy { get; set; }

        [Display(Name = "Available")]
        public bool IsAvailable { get; set; }

        // Navigation property
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
