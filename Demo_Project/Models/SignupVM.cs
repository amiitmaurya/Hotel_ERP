namespace Demo_Project.Models
{
    public class SignupVM
    {
        public string FullName { get; set; } = "";

        public string Email { get; set; } = "";

        public string Mobile { get; set; } = "";

        public string UserName { get; set; } = "";

        public string Password { get; set; } = "";

        public long RoleId { get; set; }
    }
}
