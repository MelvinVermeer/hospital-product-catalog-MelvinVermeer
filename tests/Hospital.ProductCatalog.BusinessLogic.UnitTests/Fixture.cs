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
    }
}
