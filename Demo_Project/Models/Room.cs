using Demo_Project.Models.Enums;
namespace Demo_Project.Models
{
    public class Room
    {
        public long Id { get; set; }

        public string RoomNumber { get; set; }

        public long FloorId { get; set; }

        public long RoomTypeId { get; set; }

        public decimal PricePerNight { get; set; }

        public string? Description { get; set; }

        public RoomStatus Status { get; set; } // Available, Occupied, Maintenance

        public bool IsActive { get; set; }

        public virtual Floor? Floor { get; set; }

        public virtual RoomType? RoomType { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    = new List<Booking>();

        public virtual ICollection<RoomImage> RoomImages { get; set; }
            = new List<RoomImage>();

        public virtual ICollection<RoomAmenity> RoomAmenities { get; set; }
            = new List<RoomAmenity>();
    }
}
