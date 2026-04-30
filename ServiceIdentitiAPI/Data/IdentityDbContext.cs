using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServiceIdentityAPI.Models;

namespace ServiceIdentityAPI.Data
{
    public class IdentityDbContext:DbContext
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext>options):base(options) { }
        public DbSet<UserIdentity> UserIdentity { get; set; }
        public DbSet<EmailRecord> EmailAddress { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<BusinessEntity>BusinessEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserIdentity>().ToTable("Password", "Person");
            modelBuilder.Entity<UserIdentity>().HasKey(k => k.BusinessEntityID);

            modelBuilder.Entity<EmailRecord>().ToTable("EmailAddress", "Person");
            modelBuilder.Entity<EmailRecord>().HasKey(e => e.BusinessEntityID);

            modelBuilder.Entity<Customer>().ToTable("Customer","Sales");
            modelBuilder.Entity<Customer>().HasKey(c => c.CustomerID);

            modelBuilder.Entity<BusinessEntity>().ToTable("BusinessEntity","Person");
            modelBuilder.Entity<BusinessEntity>().HasKey(be => be.BusinessEntityID);

        }
    }

}
