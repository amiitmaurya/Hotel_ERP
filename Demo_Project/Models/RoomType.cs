namespace Demo_Project.Models
{
    public class RoomType
    {
        public long Id { get; set; }

        public string RoomTypeName { get; set; }

        public string Description { get; set; }

        public int MaxGuests { get; set; }

        public decimal BasePrice { get; set; }

        public bool IsActive { get; set; }
    }
}
