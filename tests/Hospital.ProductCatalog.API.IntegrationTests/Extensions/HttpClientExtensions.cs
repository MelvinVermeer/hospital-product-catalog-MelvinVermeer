using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hospital.ProductCatalog.API.IntegrationTests.Extensions
{
    public static class HttpClientExtensions
    {
        // Interesting thing : I was used to using calling PostAsJsonAsync("/categories", category);
        // This is currently not available in the API (3.1) offered by Microsoft, since they are moving away from Newtonsoft
        // The new implementation is scheduled for the 5.0 runtime, 
        // The PR : https://github.com/dotnet/runtime/pull/33459
        // For this assignment I decided to only create a very very simplified implementation, so the tests will stil look clean. 
        private static readonly JsonSerializerOptions DefaultSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private const string JsonMediaType = "application/json";

        public static async Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, string requestUri, T value)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var request = new HttpRequestMessage(HttpMethod.Post, requestUri)
            {
                Content = new StringContent(JsonSerializer.Serialize(value, DefaultSerializerOptions), Encoding.UTF8, JsonMediaType),
            };

            return await client.SendAsync(request);
        }

        public static async Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient client, string requestUri, T value)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var request = new HttpRequestMessage(HttpMethod.Put, requestUri)
            {
                Content = new StringContent(JsonSerializer.Serialize(value, DefaultSerializerOptions), Encoding.UTF8, JsonMediaType),
            };

            return await client.SendAsync(request);
        }

        public static async Task<T> GetFromJsonAsync<T>(this HttpClient client, string requestUri)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var result = await client.GetStringAsync(requestUri);
            return JsonSerializer.Deserialize<T>(result, DefaultSerializerOptions);
        }
    }
}
