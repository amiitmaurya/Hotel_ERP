using Microsoft.EntityFrameworkCore;

namespace Demo_Project.Models
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options)
        : base(options)
        {
        }

        public DbSet<Amenity> Amenitys { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<CheckIn> CheckIns { get; set; }
        public DbSet<CheckOut> CheckOuts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<HotelSetting> HotelSettings { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomAmenity> RoomAmenitys { get; set; }
        public DbSet<RoomImage> RoomImages { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<User> Users { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User -> Role
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
    .HasOne(u => u.CreatedByUser)
    .WithMany()
    .HasForeignKey(u => u.CreatedByUserId)
    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.UpdatedByUser)
                .WithMany()
                .HasForeignKey(u => u.UpdatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Booking -> Customer
            modelBuilder.Entity<Booking>()
     .HasOne(b => b.Customer)
     .WithMany(c => c.Bookings)
     .HasForeignKey(b => b.CustomerId)
     .OnDelete(DeleteBehavior.Restrict);

            // Booking -> Room
            modelBuilder.Entity<Booking>()
     .HasOne(b => b.Room)
     .WithMany(r => r.Bookings)
     .HasForeignKey(b => b.RoomId)
     .OnDelete(DeleteBehavior.Restrict);

            // Booking -> User
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.CreatedByUser)
                .WithMany()
                .HasForeignKey(b => b.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Room -> Floor
            modelBuilder.Entity<Room>()
                .HasOne(r => r.Floor)
                .WithMany()
                .HasForeignKey(r => r.FloorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Room -> RoomType
            modelBuilder.Entity<Room>()
                .HasOne(r => r.RoomType)
                .WithMany()
                .HasForeignKey(r => r.RoomTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // RoomImage -> Room
            modelBuilder.Entity<RoomImage>()
                .HasOne(ri => ri.Room)
                 .WithMany(x => x.RoomImages)
                .HasForeignKey(ri => ri.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            // Invoice -> Booking
            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Booking)
                .WithMany()
                .HasForeignKey(i => i.BookingId)
                .OnDelete(DeleteBehavior.Restrict);

            // Composite Key
            modelBuilder.Entity<RoomAmenity>()
                .HasKey(x => new { x.RoomId, x.AmenityId });

            // RoomAmenity -> Room
            modelBuilder.Entity<RoomAmenity>()
                .HasOne(x => x.Room)
                .WithMany(r => r.RoomAmenities)
                .HasForeignKey(x => x.RoomId);

            // RoomAmenity -> Amenity
            modelBuilder.Entity<RoomAmenity>()
                .HasOne(x => x.Amenity)
                .WithMany(a => a.RoomAmenities)
                .HasForeignKey(x => x.AmenityId);

            modelBuilder.Entity<CheckIn>()
     .HasOne(c => c.Booking)
     .WithOne(b => b.CheckIn)
     .HasForeignKey<CheckIn>(c => c.BookingId);

            modelBuilder.Entity<CheckOut>()
                .HasOne(c => c.Booking)
                .WithOne(b => b.CheckOut)
                .HasForeignKey<CheckOut>(c => c.BookingId);
        }

    }
}
