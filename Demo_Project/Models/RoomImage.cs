namespace Demo_Project.Models
{
    public class RoomImage
    {
        public long Id { get; set; }

        public long RoomId { get; set; }

        public string ImagePath { get; set; }

        public bool IsPrimary { get; set; }

        public virtual Room? Room { get; set; }

    }
}
