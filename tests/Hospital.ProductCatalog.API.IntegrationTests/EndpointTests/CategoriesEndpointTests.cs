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

        [TestMethod]
        public async Task A_Category_Can_Be_Updated()
        {
            var client = NewHttpClient();
            var category = new Category { Code = 1, Description = "Consumables" };
            var updatedCategory = new Category { Code = 1, Description = "Implants" };

            await client.PostAsJsonAsync("/categories", category);

            var before = await client.GetFromJsonAsync<Category>("/categories/1");
            Assert.AreEqual(category.Description, before.Description);

            await client.PutAsJsonAsync("/categories/1", updatedCategory);

            var after = await client.GetFromJsonAsync<Category>("/categories/1");
            Assert.AreEqual(updatedCategory.Description, after.Description);
        }

        [TestMethod]
        public async Task A_Category_Can_Be_Deleted()
        {
            var client = NewHttpClient();
            var category = new Category { Code = 1, Description = "Consumables" };

            await client.PostAsJsonAsync("/categories", category);

            var before = await client.GetFromJsonAsync<IEnumerable<Category>>("/categories");
            Assert.AreEqual(1, before.Count());

            await client.DeleteAsync("/categories/1");

            var after = await client.GetFromJsonAsync<IEnumerable<Category>>("/categories");
            Assert.AreEqual(0, after.Count());
        }
    }
}
