using Microsoft.EntityFrameworkCore;
using UserServiceAPI.Models;

namespace UserServiceAPI.Data
{
    public class UserDbContext:DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<EmailRecord> EmailAddresses { get; set; }
        public DbSet<BusinessEntityAddress> BusinessEntityAddresses { get; set; }
        public DbSet<AddressRecord> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Person","Person");
            modelBuilder.Entity<User>().HasKey(k => k.BusinessEntityID);

            modelBuilder.Entity<EmailRecord>().ToTable("EmailAddress", "Person");
            modelBuilder.Entity<EmailRecord>().HasKey(e => e.BusinessEntityID);

            //Need composite key to connect BusinessID & AddressID
            modelBuilder.Entity<BusinessEntityAddress>().ToTable("BusinessEntityAddress", "Person");
            modelBuilder.Entity<BusinessEntityAddress>().HasKey(e => new { e.BusinessEntityID, e.AddressID });

            modelBuilder.Entity<AddressRecord>().ToTable("Address","Person");
            modelBuilder.Entity<AddressRecord>().HasKey(a => a.AddressID);
        }
    }
}
