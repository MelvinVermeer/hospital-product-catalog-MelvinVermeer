using Hospital.ProductCatalog.Domain.Entities;
using System.Collections.Generic;

namespace Hospital.ProductCatalog.API.IntegrationTests
{
    public class Fixture
    {
        public static List<Category> Categories = new List<Category>
        {
            new Category {
                Code = 1,
                Description = "Consumables"
            }
        };

        public static List<Product> Products = new List<Product>
        {
            new Product {
                Code = 1,
                Description = "Asperin",
                Category = Categories[0],
                UnitOfMeasurement = UnitOfMeasurement.Box
            }
        };
    }
}
