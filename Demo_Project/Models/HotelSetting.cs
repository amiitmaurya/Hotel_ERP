namespace Demo_Project.Models
{
    public class HotelSetting
    {
        public long Id { get; set; }

        public string HotelName { get; set; }

        public string Address { get; set; }

        public string ContactNumber { get; set; }

        public string Email { get; set; }

        public string? GSTNumber { get; set; }

        public string? LogoPath { get; set; }

        public string? Theme { get; set; }
    }
}
