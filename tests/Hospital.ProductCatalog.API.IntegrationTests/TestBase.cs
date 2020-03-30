using Hospital.ProductCatalog.DataAccess;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace Hospital.ProductCatalog.API.IntegrationTests
{
    public class TestBase
    {
        /// <summary>
        /// Creates a HTTP Client for a new instance of the ProductsApi with an new empty database
        /// </summary>
        /// <returns></returns>
        protected HttpClient NewHttpClient()
        {
            var factory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        // We could swap database or auth implementation here
                        // For example if this api would use MSSQL in production, we could change that to
                        // InMemory, for testing purposes. There is no need to do that right now because i
                        // am using InMemoryDatabase already.
                        using var scope = services.BuildServiceProvider().CreateScope();
                        var productsContext = scope.ServiceProvider.GetRequiredService<ProductCatalogContext>();

                        // Normally I would seed my test data from here, but for now my test suite runs well
                        // enough with an empty database
                        productsContext.Database.EnsureDeleted();
                        productsContext.Database.EnsureCreated();
                    });
                });

            return factory.CreateClient();
        }
    }
}
