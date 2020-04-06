using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hospital.ProductCatalog.API.UnitTests
{
    public static class HttpResponseExtensions
    {
        public static async Task<T> ReadAsAsync<T>(this HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            return await JsonSerializer.DeserializeAsync<T>(response.Body);
        }
    }
}
