using Demo_Project.Models.Enums;
namespace Demo_Project.Models
{
    public class Booking
    {
        public long Id { get; set; }

        public required string BookingNumber { get; set; }

        public long CustomerId { get; set; }

        public long RoomId { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public int TotalNights { get; set; }

        public int? Adults { get; set; }

        public int? Children { get; set; }

        public decimal PricePerNight { get; set; }

        public decimal TotalAmount { get; set; }

        public BookingStatus Status { get; set; }

        public string? Remarks { get; set; }

        public DateTime? CancelledOn { get; set; }

        public string? CancellationReason { get; set; }

        public long? CreatedByUserId { get; set; }

        public DateTime CreatedOn { get; set; }

        public long? UpdatedByUserId { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public virtual Customer? Customer { get; set; }

        public virtual Room? Room { get; set; }

        public virtual CheckIn? CheckIn { get; set; }

        public virtual CheckOut? CheckOut { get; set; }

        public virtual User? CreatedByUser { get; set; }
    }
}
