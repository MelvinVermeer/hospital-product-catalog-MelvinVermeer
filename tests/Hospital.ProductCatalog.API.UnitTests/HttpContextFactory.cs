using Microsoft.AspNetCore.Http;
using System.IO;

namespace Hospital.ProductCatalog.API.UnitTests
{
    public class HttpContextFactory
    {
        public static HttpContext Create()
        {
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            return context;
        }
    }
}
