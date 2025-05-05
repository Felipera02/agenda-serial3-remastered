using AgendaSerial3.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgendaSerial3.Infrastructure.Data
{
    public class AgendaContext(DbContextOptions<AgendaContext> opt) : DbContext(opt)
    {
        // public DbSet<EntityType> EntityGroupName { get; set; };
        public DbSet<User> Users { get; set; }
        public DbSet<PersonalCalendar> Calendars { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>()
        //        .HasIndex(u => u.UserName)
        //        .IsUnique();
        //}
    }
}
