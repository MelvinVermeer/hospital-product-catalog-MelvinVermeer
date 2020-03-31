using Hospital.ProductCatalog.BusinessLogic.Categories.Commands;
using Hospital.ProductCatalog.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Hospital.ProductCatalog.BusinessLogic.UnitTests.Categories.Commands
{
    [TestClass]
    public class CreateCategoryTests
    {
        private readonly CreateCategoryHandler _handler;
        private readonly ProductCatalogContext _context;

        public CreateCategoryTests()
        {
            _context = ProductCatalogContextFactory.Create();
            _handler = new CreateCategoryHandler(_context);
        }

        [TestMethod]
        public async Task Should_Create_A_Category_With_Given_Description()
        {
            var command = new CreateCategory { Description = "Implants" };
            var newCategoryCode = await _handler.Handle(command);

            Assert.IsNotNull(_context.Categories.Find(newCategoryCode));
            Assert.AreEqual(command.Description, _context.Categories.Find(newCategoryCode).Description);
        }
    }
}
