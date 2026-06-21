namespace Demo_Project.Models
{
    public class CheckIn
    {
        public long Id { get; set; }

        public long BookingId { get; set; }

        public DateTime CheckInDateTime { get; set; }

        public long CheckedInByUserId { get; set; }

        public virtual Booking? Booking { get; set; }
    }
}
