using Hospital.ProductCatalog.BusinessLogic.Categories.Queries;
using Hospital.ProductCatalog.BusinessLogic.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Hospital.ProductCatalog.BusinessLogic.UnitTests.Categories.Queries
{
    [TestClass]
    public class GetByCodeTests
    {
        private readonly GetByCodeQueryHandler _handler;

        public GetByCodeTests()
        {
            var context = ProductCatalogContextFactory.Create();
            var loggerMock = new Mock<ILogger<GetByCodeQueryHandler>>();
            _handler = new GetByCodeQueryHandler(context, loggerMock.Object);
        }

        [TestMethod]
        public async Task Should_Return_Category_with_Given_Code()
        {
            var result = await _handler.Handle(new GetByCode(1));
            Assert.AreEqual(Fixture.Categories[0].Description, result.Description);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Should_Throw_NotFound_When_Category_With_Given_Code_Does_Not_Exist()
        {
            await _handler.Handle(new GetByCode(2));
        }
    }
}
