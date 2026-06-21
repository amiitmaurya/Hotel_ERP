namespace Demo_Project.Models
{
    public class Amenity
    {
        public long Id { get; set; }

        public string AmenityName { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }
        public virtual ICollection<RoomAmenity> RoomAmenities { get; set; }
       = new List<RoomAmenity>();
    }
}
