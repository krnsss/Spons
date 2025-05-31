using egorDipl.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace egorDipl.Data
{
    public class SponsorsDbContext : DbContext
    {
        public SponsorsDbContext(DbContextOptions<SponsorsDbContext> options) : base(options)
        {
        }

        public DbSet<Company> Company { get; set; }
        public DbSet<Cooperation> Cooperation { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<EventStatus> EventStatus { get; set; }
        public DbSet<FeedBack> FeedBack { get; set; }
        public DbSet<RequestForCooperation> RequestForCooperation { get; set; }
        public DbSet<RequestStatus> RequestStatus { get; set; }
        public DbSet<StaffRole> StaffRole { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Notification> Notification { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Company>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Cooperation>()
                .HasKey(co => co.Id);

            modelBuilder.Entity<Cooperation>()
                .HasOne(co => co.Sponsor)
                .WithMany()
                .HasForeignKey(co => co.SponsorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Event>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Sponsor)
                .WithMany()
                .HasForeignKey(e => e.SponsorId)
                .OnDelete(DeleteBehavior.SetNull); 

            modelBuilder.Entity<FeedBack>()
                .HasKey(fb => fb.Id);

            modelBuilder.Entity<RequestForCooperation>()
                .HasKey(rfc => rfc.Id);

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
        }
    }
}