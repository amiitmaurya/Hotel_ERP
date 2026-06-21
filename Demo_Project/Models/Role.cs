namespace Demo_Project.Models
{
    public class Role
    {
        public long Id { get; set; }

        public string RoleName { get; set; }

        public string Description { get; set; }

        public bool DashboardPermission { get; set; }

        public bool BookingPermission { get; set; }

        public bool InvoicePermission { get; set; }

        public bool UserPermission { get; set; }

        public bool RoomPermission { get; set; }

        public bool ReportPermission { get; set; }

        public bool SettingPermission { get; set; }
    }
}
