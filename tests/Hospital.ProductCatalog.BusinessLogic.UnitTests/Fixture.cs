using Hospital.ProductCatalog.Domain.Entities;
using System.Collections.Generic;

namespace Hospital.ProductCatalog.BusinessLogic.UnitTests
{
    public class Fixture
    {
        public static List<Category> Categories = new List<Category>
        {
            new Category { Code = 1, Description = "Consumables" }
        };

        public static List<Product> Products = new List<Product>
        {
            new Product {
                Code = 1,
                Description = "Asperin",
                Category = Categories[0],
                Barcodes = new List<Barcode> { new Barcode { Code = "000" } }
            }
        };
    }
}
