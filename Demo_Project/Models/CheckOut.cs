namespace Demo_Project.Models
{
    public class CheckOut
    {
        public long Id { get; set; }

        public long BookingId { get; set; }

        public DateTime CheckOutDateTime { get; set; }

        public decimal ExtraCharges { get; set; }

        public string? Remarks { get; set; }

        public long CheckedOutByUserId { get; set; }

        public virtual Booking? Booking { get; set; }
    }
}
