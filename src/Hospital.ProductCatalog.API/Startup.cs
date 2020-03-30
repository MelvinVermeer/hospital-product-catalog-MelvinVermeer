using Hospital.ProductCatalog.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace Hospital.ProductCatalog.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Let's go for an InMemory Database, this way we can use Entity Framework 
            // all the way through and whenever needed this can easily be swapped by real MSSQL.
            services.AddDbContext<ProductCatalogContext>(options => options.UseInMemoryDatabase("HospitalProductCatalog"));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Hospital Product Catalog",
                    Version = "v1",
                    Description = "A Sample implementation for the Hospital Product Catalog, created for a code assignment by Soulve Innovations HR",
                    Contact = new OpenApiContact
                    {
                        Name = "Melvin Vermeer",
                        Email = "melvin.vermeer@gmail.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "GNU General Public License v3.0",
                        Url = new Uri("https://raw.githubusercontent.com/soulve-innovations/hospital-product-catalog-MelvinVermeer/master/LICENSE"),
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Hospital Product Catalog V1");
            });

            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
