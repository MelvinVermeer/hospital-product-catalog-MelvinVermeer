using Hospital.ProductCatalog.API.IntegrationTests.Extensions;
using Hospital.ProductCatalog.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ProductCatalog.API.IntegrationTests.EndpointTests
{
    [TestClass]
    public class CategoriesEndPointTests : TestBase
    {
        [TestMethod]
        public async Task A_New_Category_Can_Be_Added()
        {
            var client = NewHttpClient();
            var category = new Category { Code = 1, Description = "Consumables" };

            var before = await client.GetFromJsonAsync<IEnumerable<Category>>("/categories");
            Assert.AreEqual(0, before.Count());

            await client.PostAsJsonAsync("/categories", category);

            var after = await client.GetFromJsonAsync<IEnumerable<Category>>("/categories");
            Assert.AreEqual(1, after.Count());
        }
    }
}
