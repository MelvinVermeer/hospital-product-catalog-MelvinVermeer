﻿using Hospital.ProductCatalog.BusinessLogic.Products.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ProductCatalog.BusinessLogic.UnitTests.Products.Queries
{
    [TestClass]
    public class GetAllTests
    {
        private readonly GetAllQueryHandler _handler;

        public GetAllTests()
        {
            var context = ProductCatalogContextFactory.Create();
            var mapper = MapperFactory.Create();
            _handler = new GetAllQueryHandler(context, mapper);
        }

        [TestMethod]
        public async Task Should_Return_All_Products()
        {
            var result = await _handler.Handle(new GetAll());
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(Fixture.Products[0].Code, result[0].Code);
            Assert.AreEqual(Fixture.Products[0].Description, result[0].Description);
        }
    }
}
