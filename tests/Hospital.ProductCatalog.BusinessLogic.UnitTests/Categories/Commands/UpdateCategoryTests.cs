using Hospital.ProductCatalog.BusinessLogic.Categories.Commands;
using Hospital.ProductCatalog.BusinessLogic.Exceptions;
using Hospital.ProductCatalog.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Hospital.ProductCatalog.BusinessLogic.UnitTests.Categories.Commands
{
    [TestClass]
    public class UpdateCategoryTests
    {
        private readonly UpdateCategoryHandler _handler;
        private readonly ProductCatalogContext _context;

        public UpdateCategoryTests()
        {
            _context = ProductCatalogContextFactory.Create();
            _handler = new UpdateCategoryHandler(_context);
        }

        [TestMethod]
        public async Task Should_Update_A_Category_Description()
        {
            await _handler.Handle(new UpdateCategory { Code = 1, Description = "Implants" });
            Assert.AreEqual("Implants", _context.Categories.Find(1).Description);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Should_Throw_NotFound_When_Category_With_Given_Code_Does_Not_Exist()
        {
            await _handler.Handle(new UpdateCategory { Code = 2 });
        }
    }
}
