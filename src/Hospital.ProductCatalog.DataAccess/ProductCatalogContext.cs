using Hospital.ProductCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hospital.ProductCatalog.DataAccess
{
    public class ProductCatalogContext : DbContext
    {
        public ProductCatalogContext(DbContextOptions<ProductCatalogContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // For this Assignement I prefer the configuration with the Fluent api over Attributes
            // 1) It is more explicit and shows intent better than attributes 
            // 2) I don't want to leak entity framework details ono the domain objects
            modelBuilder.Entity<Category>().HasKey(x => x.Code);
            modelBuilder.Entity<Product>().HasKey(x => x.Code);
            modelBuilder.Entity<Barcode>().HasKey(x => x.Code);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Barcodes)
                .WithOne()
                .HasForeignKey(b => b.ProductCode);
        }
    }
}
