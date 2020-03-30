using Hospital.ProductCatalog.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;

namespace Hospital.ProductCatalog.BusinessLogic.UnitTests
{
    public class ProductCatalogContextFactory
    {
        public static ProductCatalogContext Create()
        {
            var options = new DbContextOptionsBuilder<ProductCatalogContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            // Seed in-mem test database
            var context = new ProductCatalogContext(options);

            context.Database.EnsureCreated();
            context.Categories.AddRange(Fixture.Categories);
            context.SaveChanges();
            
            return context;
        }

        public static void Destroy(ProductCatalogContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
