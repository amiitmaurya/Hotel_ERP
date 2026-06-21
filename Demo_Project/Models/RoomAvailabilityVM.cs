using Demo_Project.Models.Enums;

namespace Demo_Project.Models
{
    public class RoomAvailabilityVM
    {
        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public RoomStatus RoomStatus { get; set; }

        public List<Room> AvailableRooms { get; set; } = new();
    }
}
