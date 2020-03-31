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
            var category = new Category { Code = 2, Description = "Implants" };

            var response = await client.PostAsJsonAsync("/categories", category);

            var result = await client.GetFromJsonAsync<Category>(response.Headers.Location.ToString());
            Assert.AreEqual(category.Description, result.Description);
        }

        [TestMethod]
        public async Task A_Category_Can_Be_Updated()
        {
            var client = NewHttpClient();
            var updatedCategory = new Category { Code = 1, Description = "new description" };
            await client.PutAsJsonAsync("/categories/1", updatedCategory);

            var result = await client.GetFromJsonAsync<Category>("/categories/1");
            Assert.AreEqual(updatedCategory.Description, result.Description);
        }

        [TestMethod]
        public async Task A_Category_Can_Be_Deleted()
        {
            var client = NewHttpClient();
            await client.DeleteAsync("/categories/1");

            var after = await client.GetFromJsonAsync<IEnumerable<Category>>("/categories");
            Assert.AreEqual(0, after.Count());
        }
    }
}
