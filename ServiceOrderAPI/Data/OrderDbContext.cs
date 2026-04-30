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
            modelBuilder.Entity<OrderDetail>()
                .ToTable("SalesOrderDetail", "Sales", t => t.HasTrigger("iduSalesOrderDetail"))
                .HasKey(d => d.SalesOrderDetailID);
            modelBuilder.Entity<OrderDetail>()
                .Property(d => d.LineTotal)
                .ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<OrderHeader>()
                .ToTable("SalesOrderHeader", "Sales", t => t.HasTrigger("uSalesOrderHeader"))
                .HasKey(o => o.SalesOrderID);
            modelBuilder.Entity<OrderHeader>()
                .Property(o => o.TotalDue)
                .ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<OrderHeader>()
                .HasMany(o => o.OrderLines)
                .WithOne()
                .HasForeignKey(d => d.SalesOrderID);
        }
    }
}
