using Hospital.ProductCatalog.DataAccess;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

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

                        // Clear and re-seed the database, to write predictable tests whenever we ask a NewHttpClient()
                        productsContext.Database.EnsureDeleted();
                        productsContext.Database.EnsureCreated();
                        productsContext.Categories.AddRange(Fixture.Categories);
                        productsContext.Products.AddRange(Fixture.Products);
                        productsContext.SaveChanges();
                    });
                });

            return factory.CreateClient();
        }

        protected HttpClient NewAuthenticatedHttpClient()
        {
            var client = NewHttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {CreateBearerToken()}");
            return client;
        }

        private string CreateBearerToken()
        {
            // In any production situation will not store our signing key in source control.
            var key = Encoding.ASCII.GetBytes("SecretSigningKey");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "test user")
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }
    }
}
