using AutoMapper;
using FluentValidation;
using Hospital.ProductCatalog.API.Middleware;
using Hospital.ProductCatalog.BusinessLogic;
using Hospital.ProductCatalog.BusinessLogic.Categories.Queries;
using Hospital.ProductCatalog.DataAccess;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

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

            // Any reference will do here. I prefer a type reference over Assembly name, to prevent magic strings 
            services.AddMediatR(typeof(GetAll));
            services.AddAutoMapper(typeof(HospitalProductMappingProfile));
            services.AddValidatorsFromAssemblyContaining(typeof(GetAll));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    // In any production situation will not store our signing key in source control.
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("SecretSigningKey")), 
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddAuthorization();

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
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Hospital Product Catalog V1");
            });

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
