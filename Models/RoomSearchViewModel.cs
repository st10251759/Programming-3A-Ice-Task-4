using System.ComponentModel.DataAnnotations;

namespace Hotel_Booking_Prog_7311_Ice_Task_4.Models
{
    public class RoomSearchViewModel
    {
        [Required]
        [Display(Name = "Check-in Date")]
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }

        [Required]
        [Display(Name = "Check-out Date")]
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }

        [Display(Name = "Number of Guests")]
        [Range(1, 10)]
        public int Guests { get; set; }

        public List<Room> AvailableRooms { get; set; }
    }
}
