using Hospital.ProductCatalog.API.IntegrationTests.Extensions;
using Hospital.ProductCatalog.BusinessLogic.Products.Commands;
using Hospital.ProductCatalog.BusinessLogic.Products.Queries;
using Hospital.ProductCatalog.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ProductCatalog.API.IntegrationTests.EndpointTests
{
    [TestClass]
    public class ProductsEndPointTests : TestBase
    {
        [TestMethod]
        public async Task A_New_Product_Can_Be_Added()
        {
            var client = NewHttpClient();
            var product = new CreateProduct { 
                Description = "Asperin", 
                CategoryCode = Fixture.Categories[0].Code,
                UnitOfMeasurement = "box" 
            };
           
            await client.PostAsJsonAsync("/products", product);

            var result = await client.GetFromJsonAsync<ProductDTO>("/products/2");
            Assert.AreEqual(product.Description, result.Description);
        }

        [TestMethod]
        public async Task A_Product_Can_Be_Updated()
        {
            var client = NewHttpClient();
            var updatedProduct = new UpdateProduct {
                Code = 1,
                Description = "Ibuprofen",
                CategoryCode = Fixture.Categories[0].Code,
                UnitOfMeasurement = "box"
            };
            await client.PutAsJsonAsync("/products/1", updatedProduct);

            var result = await client.GetFromJsonAsync<ProductDTO>("/products/1");
            Assert.AreEqual(updatedProduct.Description, result.Description);
        }

        [TestMethod]
        public async Task A_Product_Can_Be_Deleted()
        {
            var client = NewHttpClient();
            await client.DeleteAsync("/products/1");

            var after = await client.GetFromJsonAsync<IEnumerable<ProductDTO>>("/products");
            Assert.AreEqual(0, after.Count());
        }
    }
}
