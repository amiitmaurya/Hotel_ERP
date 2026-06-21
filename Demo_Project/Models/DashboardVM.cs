namespace Demo_Project.Models
{
    public class DashboardVM
    {
        public DateTime? CheckInDate { get; set; }

        public DateTime? CheckOutDate { get; set; }

        public int TotalRooms { get; set; }

        public int AvailableRooms { get; set; }

        public int Maintenance { get; set; }

        public int ReservedRooms { get; set; }

        public int TodayCheckIns { get; set; }

        public int TodayCheckOuts { get; set; }
    }
}
