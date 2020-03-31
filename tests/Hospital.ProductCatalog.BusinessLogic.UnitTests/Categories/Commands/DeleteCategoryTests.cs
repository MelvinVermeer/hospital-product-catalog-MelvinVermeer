using Hospital.ProductCatalog.BusinessLogic.Categories.Commands;
using Hospital.ProductCatalog.BusinessLogic.Exceptions;
using Hospital.ProductCatalog.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ProductCatalog.BusinessLogic.UnitTests.Categories.Commands
{
    [TestClass]
    public class DeleteCategoryTests
    {
        private readonly DeleteCategoryHandler _handler;
        private readonly ProductCatalogContext _context;

        public DeleteCategoryTests()
        {
            _context = ProductCatalogContextFactory.Create();
            _handler = new DeleteCategoryHandler(_context);
        }

        [TestMethod]
        public async Task Should_Delete_A_Category()
        {
            await _handler.Handle(new DeleteCategory(1));
            Assert.AreEqual(0, _context.Categories.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Should_Throw_NotFound_When_Category_With_Given_Code_Does_Not_Exist()
        {
            await _handler.Handle(new DeleteCategory(2));
        }
    }
}
