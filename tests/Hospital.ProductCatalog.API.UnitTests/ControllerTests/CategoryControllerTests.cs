using Hospital.ProductCatalog.API.Controllers;
using Hospital.ProductCatalog.DataAccess;
using Hospital.ProductCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ProductCatalog.API.UnitTests.ControllerTests
{
    [TestClass]
    public class CategoryControllerTests
    {
        private Category _category = new Category { Code = 1, Description = "Consumables" };

        [TestMethod]
        public async Task Get_Should_Return_All_Categories()
        {
            var options = new DbContextOptionsBuilder<ProductCatalogContext>()
                .UseInMemoryDatabase("Get_Should_Return_All_Categories")
                .Options;

            using (var db = new ProductCatalogContext(options))
            {
                db.Categories.Add(_category);
                db.SaveChanges();
            }

            using (var context = new ProductCatalogContext(options))
            {
                var controller = new CategoriesController(context);
                var result = (await controller.Get()).Value;

                Assert.AreEqual(1, result.Count());
                Assert.AreEqual(_category.Code, result.First().Code);
            }
        }
    }
}
