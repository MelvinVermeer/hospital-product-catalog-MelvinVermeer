using Hospital.ProductCatalog.BusinessLogic.Products.Queries;
using Hospital.ProductCatalog.BusinessLogic.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Hospital.ProductCatalog.BusinessLogic.UnitTests.Products.Queries
{
    [TestClass]
    public class GetByCodeTests
    {
        private readonly GetByCodeQueryHandler _handler;

        public GetByCodeTests()
        {
            var context = ProductCatalogContextFactory.Create();
            var mapper = MapperFactory.Create();
            _handler = new GetByCodeQueryHandler(context, mapper);
        }

        [TestMethod]
        public async Task Should_Return_Category_with_Given_Code()
        {
            var result = await _handler.Handle(new GetByCode(1));
            Assert.AreEqual(Fixture.Products[0].Description, result.Description);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Should_Throw_NotFound_When_Category_With_Given_Code_Does_Not_Exist()
        {
            await _handler.Handle(new GetByCode(2));
        }
    }
}
