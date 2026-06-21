using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Demo_Project.Models
{
    public class RoomImageVM
    {
        public long RoomId { get; set; }

        [Required]
        public IFormFile? ImageFile { get; set; }

        public bool IsPrimary { get; set; }
        public List<SelectListItem>? Rooms { get; set; }
    }
}
