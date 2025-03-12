namespace Hotel_Booking_Prog_7311_Ice_Task_4.Models
{
    public class DashboardViewModel
    {
        public int TotalRooms { get; set; }
        public int AvailableRooms { get; set; }
        public int TotalBookings { get; set; }
        public decimal Revenue { get; set; }
        public List<Booking> RecentBookings { get; set; }
    }
}
