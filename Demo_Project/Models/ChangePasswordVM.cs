using System.ComponentModel.DataAnnotations;

namespace Demo_Project.Models
{
    public class ChangePasswordVM
    {
        public string UserName { get; set; } = "";
        public string OldPassword { get; set; } = "";

        [Required]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; } = "";

        [Required]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = "";
    }
}
