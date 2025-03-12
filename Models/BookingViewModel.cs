namespace Hotel_Booking_Prog_7311_Ice_Task_4.Models
{
    // View Models
    public class BookingViewModel
    {
        public Room Room { get; set; }
        public Booking Booking { get; set; }
        public Customer Customer { get; set; }
        public List<Room> AvailableRooms { get; set; }
    }
}
