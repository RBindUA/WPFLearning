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
        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserIdentity>().ToTable("Password", "Person");
            modelBuilder.Entity<UserIdentity>().HasKey(k => k.BusinessEntityID);

            modelBuilder.Entity<EmailRecord>().ToTable("EmailAddress", "Person",
                t => t.HasTrigger("iuEmailAddress"));
            modelBuilder.Entity<EmailRecord>().HasKey(e => e.BusinessEntityID);

            modelBuilder.Entity<Customer>().ToTable("Customer","Sales",
                t => t.HasTrigger("iuCustomer"));
            modelBuilder.Entity<Customer>().HasKey(c => c.CustomerID);
            modelBuilder.Entity<Customer>().Property(c => c.AccountNumber)
                .ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<BusinessEntity>().ToTable("BusinessEntity","Person",
                t => t.HasTrigger("tr_BusinessEntity_ModifiedDate"));
            modelBuilder.Entity<BusinessEntity>().HasKey(be => be.BusinessEntityID);

            modelBuilder.Entity<Person>().ToTable("Person","Person",
                t => t.HasTrigger("iuPerson"));
            modelBuilder.Entity<Person>().HasKey(be => be.BusinessEntityID);

        }
    }

}
