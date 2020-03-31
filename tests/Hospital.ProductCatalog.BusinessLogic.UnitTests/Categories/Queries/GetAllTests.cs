using Hospital.ProductCatalog.BusinessLogic.Categories.Queries;
using Hospital.ProductCatalog.BusinessLogic.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ProductCatalog.BusinessLogic.UnitTests.Categories.Queries
{
    [TestClass]
    public class GetAllTests
    {
        private readonly GetAllQueryHandler _handler;

        public GetAllTests()
        {
            var context = ProductCatalogContextFactory.Create();
            _handler = new GetAllQueryHandler(context);
        }

        [TestMethod]
        public async Task Should_Return_All_Categories()
        {
            var result = await _handler.Handle(new GetAll());
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(Fixture.Categories[0].Code, result[0].Code);
            Assert.AreEqual(Fixture.Categories[0].Description, result[0].Description);
        }
    }
}
