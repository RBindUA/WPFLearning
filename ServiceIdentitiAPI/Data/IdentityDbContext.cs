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
        public DbSet<UserIdentity> UserIdentitiy { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserIdentity>().ToTable("Password", "Person");
            modelBuilder.Entity<UserIdentity>().HasKey(k => k.BusinessEntityID);
        }
    }

}
