using Demo_Project.Models.Enums;
namespace Demo_Project.Models
{
    public class Invoice
    {
        public long Id { get; set; }

        public string InvoiceNumber { get; set; }

        public long BookingId { get; set; }

        public DateTime InvoiceDate { get; set; }

        public decimal RoomAmount { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal GrandTotal { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public string PaymentMode { get; set; }

        public virtual Booking? Booking { get; set; }
    }
}
