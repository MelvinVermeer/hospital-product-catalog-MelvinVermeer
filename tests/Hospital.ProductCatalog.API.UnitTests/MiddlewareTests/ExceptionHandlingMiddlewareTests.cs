using Hospital.ProductCatalog.API.Middleware;
using Hospital.ProductCatalog.BusinessLogic.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public async Task Write_404_Response_If_NotFound_Is_Thrown_v2()
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
    }
}
