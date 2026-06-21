namespace Demo_Project.Models
{
    public class User
    {
        public long Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public long RoleId { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }

        public long? CreatedByUserId { get; set; }
        public long? UpdatedByUserId { get; set; }

        public virtual Role? Role { get; set; }

        public virtual User? CreatedByUser { get; set; }
        public virtual User? UpdatedByUser { get; set; }
    }
}
