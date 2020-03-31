using Hospital.ProductCatalog.BusinessLogic.Exceptions;
using Hospital.ProductCatalog.BusinessLogic.Products.Commands;
using Hospital.ProductCatalog.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ProductCatalog.BusinessLogic.UnitTests.Products.Commands
{
    [TestClass]
    public class DeleteProductTests
    {
        private readonly DeleteProductHandler _handler;
        private readonly ProductCatalogContext _context;

        public DeleteProductTests()
        {
            _context = ProductCatalogContextFactory.Create();
            _handler = new DeleteProductHandler(_context);
        }

        [TestMethod]
        public async Task Should_Delete_A_Product()
        {
            await _handler.Handle(new DeleteProduct(1));
            Assert.AreEqual(0, _context.Products.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Should_Throw_NotFound_When_Product_With_Given_Code_Does_Not_Exist()
        {
            await _handler.Handle(new DeleteProduct(2));
        }
    }
}
