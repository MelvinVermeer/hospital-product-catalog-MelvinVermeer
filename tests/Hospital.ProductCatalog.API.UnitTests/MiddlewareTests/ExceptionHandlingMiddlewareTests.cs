using FluentValidation;
using FluentValidation.Results;
using Hospital.ProductCatalog.API.Middleware;
using Hospital.ProductCatalog.BusinessLogic.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hospital.ProductCatalog.API.UnitTests.MiddlewareTests
{
    [TestClass]
    public class ExceptionHandlingMiddlewareTests
    {
        [TestMethod]
        public async Task Call_Next_If_No_Exception()
        {
            var isCalled = false;
            Task next(HttpContext context)
            {
                isCalled = true;
                return Task.FromResult(0);
            }
            var middleware = new ExceptionHandlingMiddleware(next);

            await middleware.Invoke(null);
            Assert.IsTrue(isCalled);
        }

        [TestMethod]
        public async Task Write_404_Response_If_NotFound_Is_Thrown()
        {
            var exception = new NotFoundException("Category", 1);
            Task next(HttpContext context)
            {
                throw exception;
            }
            var middleware = new ExceptionHandlingMiddleware(next);
            var context = HttpContextFactory.Create();

            await middleware.Invoke(context);

            Assert.AreEqual("application/json", context.Response.ContentType);
            Assert.AreEqual(404, context.Response.StatusCode);
            Assert.AreEqual(exception.Message, await context.Response.ReadAsAsync<string>());
        }

        [TestMethod]
        public async Task Write_400_Response_If_ValidationException_Is_Thrown()
        {
            var failures = new List<ValidationFailure> 
            {
                new ValidationFailure("name", "name can not be empty")
            };

            var exception = new ValidationException(failures);

            Task next(HttpContext context)
            {
                throw exception;
            }
            var middleware = new ExceptionHandlingMiddleware(next);
            var context = HttpContextFactory.Create();

            await middleware.Invoke(context);

            Assert.AreEqual("application/json", context.Response.ContentType);
            Assert.AreEqual(400, context.Response.StatusCode);

            var failuresResponse = await context.Response.ReadAsAsync<List<ValidationFailure>>();

            Assert.AreEqual(exception.Errors.ToList()[0].ErrorMessage, failuresResponse[0].ErrorMessage);
        }

        [TestMethod]
        public async Task Write_500_Response_If_Anonther_Is_Thrown()
        {
            var exception = new Exception("input invalid");
            Task next(HttpContext context)
            {
                throw exception;
            }
            var middleware = new ExceptionHandlingMiddleware(next);
            var context = HttpContextFactory.Create();

            await middleware.Invoke(context);

            Assert.AreEqual("application/json", context.Response.ContentType);
            Assert.AreEqual(500, context.Response.StatusCode);
            Assert.AreEqual(exception.Message, await context.Response.ReadAsAsync<string>());
        }
    }
}
