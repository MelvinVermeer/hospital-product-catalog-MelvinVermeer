using Hospital.ProductCatalog.BusinessLogic.Categories.Queries;
using Hospital.ProductCatalog.BusinessLogic.Exceptions;
using Hospital.ProductCatalog.DataAccess;
using Hospital.ProductCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Hospital.ProductCatalog.BusinessLogic.UnitTests.Categories.Queries
{
    [TestClass]
    public class GetByCodeTests
    {
        private Category _category = new Category { Code = 1, Description = "Consumables" };

        [TestMethod]
        public async Task Should_Return_Category_with_Given_Code()
        {
            var options = new DbContextOptionsBuilder<ProductCatalogContext>()
                .UseInMemoryDatabase("Get_Should_Return_Category_With_Given_Code")
                .Options;

            using (var db = new ProductCatalogContext(options))
            {
                db.Categories.Add(_category);
                db.SaveChanges();
            }

            using (var context = new ProductCatalogContext(options))
            {
                var handler = new GetByCodeQueryHandler(context);
                var result = await handler.Handle(new GetByCode(_category.Code));
                Assert.AreEqual(_category.Description, result.Description);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Should_Throw_NotFound_When_Category_With_Given_Code_Does_Not_Exist()
        {
            var options = new DbContextOptionsBuilder<ProductCatalogContext>()
               .UseInMemoryDatabase("Should_Throw_NotFound_When_Category_With_Given_Code_Does_Not_Exist")
               .Options;

            using (var context = new ProductCatalogContext(options))
            {
                var handler = new GetByCodeQueryHandler(context);
                var result = await handler.Handle(new GetByCode(_category.Code));
            }
        }
    }
}
