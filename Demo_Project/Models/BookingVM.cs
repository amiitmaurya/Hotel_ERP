namespace Demo_Project.Models
{
    using Microsoft.AspNetCore.Http;
    public class BookingVM
    {
        public long RoomId { get; set; }

        public string RoomNumber { get; set; } = "";

        public string RoomType { get; set; } = "";

        public decimal PricePerNight { get; set; }


        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public int TotalNights { get; set; }

        public decimal TotalAmount { get; set; }

        public int Adults { get; set; }

        public int Children { get; set; }

        // Customer Details

        public string FullName { get; set; } = "";

        public string Mobile { get; set; } = "";

        public string Email { get; set; } = "";

        public string Gender { get; set; } = "";
        public DateTime? DateOfBirth { get; set; }

        public string Address { get; set; } = "";

        public string City { get; set; } = "";

        public string State { get; set; } = "";

        public string Country { get; set; } = "";

        public string Pincode { get; set; } = "";

        public string IdentityType { get; set; } = "";

        public string IdentityNumber { get; set; } = "";
        public IFormFile? IdentityImage { get; set; } = null;

        public string? IdentityImagePath { get; set; } = "";


        public string? EmergencyContactName { get; set; } = "";

        public string? EmergencyContactMobile { get; set; } = "";
        public long CreatedByUserId { get; set; } = 0;
    }
}
