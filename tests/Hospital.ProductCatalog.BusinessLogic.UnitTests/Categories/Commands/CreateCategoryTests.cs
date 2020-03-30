using Hospital.ProductCatalog.BusinessLogic.Categories.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Hospital.ProductCatalog.BusinessLogic.UnitTests.Categories.Queries
{
    [TestClass]
    public class CreateCategoryTests
    {
        private readonly CreateCategoryHandler _handler;

        public CreateCategoryTests()
        {
            var context = ProductCatalogContextFactory.Create();
            _handler = new CreateCategoryHandler(context);
        }

        [TestMethod]
        public async Task Should_Create_A_Category_With_Given_Description()
        {
            var result = await _handler.Handle(new CreateCategory { Description = "Implants" });
            Assert.AreEqual(2, result);
        }
    }
}
