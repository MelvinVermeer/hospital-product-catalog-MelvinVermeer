using Hospital.ProductCatalog.BusinessLogic.Exceptions;
using Hospital.ProductCatalog.BusinessLogic.Products.Commands;
using Hospital.ProductCatalog.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital.ProductCatalog.BusinessLogic.UnitTests.Products.Commands
{
    [TestClass]
    public class UpdateProductTests
    {
        private readonly UpdateProductHandler _handler;
        private readonly ProductCatalogContext _context;

        public UpdateProductTests()
        {
            _context = ProductCatalogContextFactory.Create();
            var mapper = MapperFactory.Create();
            _handler = new UpdateProductHandler(_context, mapper);
        }

        [TestMethod]
        public async Task Should_Update_A_Product_Description()
        {
            var command = new UpdateProduct
            {
                Code = Fixture.Products[0].Code,
                Barcodes = new List<string> { "000" },
                Description = "Implants",
                UnitOfMeasurement = "Box",
                CategoryCode = Fixture.Categories[0].Code
            };
            await _handler.Handle(command);
            Assert.AreEqual("Implants", _context.Products.Find(1).Description);
        }

        [TestMethod]
        public async Task Should_Update_A_Product_Barcodes()
        {
            var command = new UpdateProduct
            {
                Code = Fixture.Products[0].Code,
                Barcodes = new List<string> { "000", "111" },
                Description = "Implants",
                UnitOfMeasurement = "Box",
                CategoryCode = Fixture.Categories[0].Code
            };

            await _handler.Handle(command);

            Assert.AreEqual(command.Barcodes[0], _context.Products.Find(1).Barcodes[0].ToString());
            Assert.AreEqual(command.Barcodes[1], _context.Products.Find(1).Barcodes[1].ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Should_Throw_NotFound_When_Product_With_Given_Code_Does_Not_Exist()
        {
            await _handler.Handle(new UpdateProduct { Code = 2 });
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Should_Throw_NotFound_If_No_Category_With_Given_Code_Exisits()
        {
            await _handler.Handle(new UpdateProduct { Code = 1, CategoryCode = 99 });
        }
    }
}
