using Microsoft.EntityFrameworkCore;
using ServiceProductAPI.Models;

namespace ServiceProductAPI.Data
{
    public class ProductDbContext:DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDescription> ProductDescriptions { get; set; }
        public DbSet<ProductModelProductDescriptionCulture> productModelProductDescriptionCultures { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Product","Production");
            modelBuilder.Entity<Product>().HasKey(p => p.ProductID);

            modelBuilder.Entity<ProductDescription>().ToTable("ProductDescription","Production");
            modelBuilder.Entity<ProductDescription>().HasKey(pd => pd.ProductDescriptionID);

            //Don`t have the same key for product and description. Junction table 
            modelBuilder.Entity<ProductModelProductDescriptionCulture>()
                .ToTable("ProductModelProductDescriptionCulture", "Production")
                .HasKey(pc => new { pc.ProductModelID, pc.ProductDescriptionID, pc.CultureID });
        }
    }
}
