using Demo_Project.Models.Enums;

namespace Demo_Project.Models
{
    public class ManageBookingVM
    {
        public long Id { get; set; }

        public string BookingNumber { get; set; } = "";

        public string CustomerName { get; set; } = "";

        public string Mobile { get; set; } = "";

        public string RoomNumber { get; set; } = "";

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public int? Adults { get; set; }

        public int? Children { get; set; }

        public decimal TotalAmount { get; set; }

        public BookingStatus Status { get; set; }
    }
}
