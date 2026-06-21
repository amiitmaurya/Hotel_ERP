namespace Demo_Project.Models
{
    public class RoomAmenity
    {
        public long RoomId { get; set; }

        public long AmenityId { get; set; }

        public virtual Room? Room { get; set; }

        public virtual Amenity? Amenity { get; set; }

    }
}
