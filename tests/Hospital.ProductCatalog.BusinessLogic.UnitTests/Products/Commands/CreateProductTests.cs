using AutoMapper;
using Hospital.ProductCatalog.BusinessLogic.Exceptions;
using Hospital.ProductCatalog.BusinessLogic.Products.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Hospital.ProductCatalog.BusinessLogic.UnitTests.Products.Queries
{
    [TestClass]
    public class CreateProductTests
    {
        private readonly CreateProductHandler _handler;

        public CreateProductTests()
        {
            var context = ProductCatalogContextFactory.Create();
            var mapper = MapperFactory.Create();

            _handler = new CreateProductHandler(context, mapper);
        }

        [TestMethod]
        public async Task Should_Create_A_Product()
        {
            var command = new CreateProduct
            {
                Description = "Implants",
                UnitOfMeasurement = "Box",
                CategoryCode = Fixture.Categories[0].Code
            };
            var result = await _handler.Handle(command);
            // Use a better assertion
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Should_Throw_NotFound_If_No_Category_With_Given_Code_Exisits()
        {
            await _handler.Handle(new CreateProduct { Description = "Implants", CategoryCode = 99 });
        }
    }
}
