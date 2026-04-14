using Microsoft.EntityFrameworkCore;
using ServiceOrderAPI.Models;

namespace ServiceOrderAPI.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>().ToTable("SalesOrderDetail", "Sales")
                .HasKey(d => d.SalesOrderDetailID);

            modelBuilder.Entity<OrderHeader>().ToTable("SalesOrderHeader", "Sales")
               .HasKey(o => o.SalesOrderID);

            modelBuilder.Entity<OrderHeader>()
                .HasMany(o => o.OrderLines)
                .WithOne()
                .HasForeignKey(d => d.SalesOrderID);
        }
    }
}
