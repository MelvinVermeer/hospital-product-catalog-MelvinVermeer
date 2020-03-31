using AutoMapper;
using Hospital.ProductCatalog.BusinessLogic.Exceptions;
using Hospital.ProductCatalog.BusinessLogic.Products.Commands;
using Hospital.ProductCatalog.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Hospital.ProductCatalog.BusinessLogic.UnitTests.Products.Queries
{
    [TestClass]
    public class CreateProductTests
    {
        private readonly ProductCatalogContext _context;
        private readonly CreateProductHandler _handler;

        public CreateProductTests()
        {
            _context = ProductCatalogContextFactory.Create();
            var mapper = MapperFactory.Create();
            _handler = new CreateProductHandler(_context, mapper);
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
            var newProductCode = await _handler.Handle(command);

            Assert.IsNotNull(_context.Products.Find(newProductCode));
            Assert.AreEqual(command.Description, _context.Products.Find(newProductCode).Description);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Should_Throw_NotFound_If_No_Category_With_Given_Code_Exisits()
        {
            await _handler.Handle(new CreateProduct { Description = "Implants", CategoryCode = 99 });
        }
    }
}
