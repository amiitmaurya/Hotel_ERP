namespace Demo_Project.Models
{
    public class Customer
    {
        public long Id { get; set; }

        public string FullName { get; set; }

        public string Mobile { get; set; }

        public string? Email { get; set; }

        public string Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string Pincode { get; set; }

        public string IdentityType { get; set; }

        public string IdentityNumber { get; set; }

        public string IdentityImagePath { get; set; }

        public DateTime CreatedOn { get; set; }
        public string? EmergencyContactName { get; set; }

        public string? EmergencyContactMobile { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        = new List<Booking>();
    }
}
