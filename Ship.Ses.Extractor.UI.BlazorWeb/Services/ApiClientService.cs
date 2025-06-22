using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ship.Ses.Extractor.UI.BlazorWeb.Services
{

    public class ApiClientService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly LocalStorageService _localStorageService;

        private const string EmrConnectionIdHeaderName = "X-Emr-Connection-Id";

        public ApiClientService(HttpClient httpClient, LocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        private async Task AddEmrConnectionHeaderIfNeeded(HttpRequestMessage request)
        {
            if (request.RequestUri == null)
            {
                // Should not happen with valid HttpClient usage, but good for null safety
                return;
            }

            // Get the absolute URI if it's relative to the base address
            // The request.RequestUri here is what was passed to GetAsync/PostAsync etc.
            // If HttpClient has a BaseAddress, it will combine with relative URIs.
            // However, request.RequestUri.LocalPath still won't work on relative URIs.
            // So, we'll construct the full URI or check the string representation.

            // Option 1: Check the string representation of the relative URI directly
            // This is generally safer and avoids issues with LocalPath on relative URIs.
            string relativePath = request.RequestUri.OriginalString; // Gives you "emr/tables" or "mappings/resource-types"

            // Alternatively, you can combine with base address for a full URI check if needed
            // Uri fullUri = new Uri(_httpClient.BaseAddress, request.RequestUri);
            // string fullPath = fullUri.AbsolutePath; // This will give you /api/v1/emr/tables etc.

            // Only add the header for EMR-related endpoints (adjust these paths as needed)
            if (relativePath.StartsWith("emr/tables") ||
                relativePath.StartsWith("emr/test-connection") ||
                relativePath.StartsWith("emr/connections")) // Added /emr/connections for creation/update/delete/select if they also need a specific connection implicitly
            {
                var connectionId = await _localStorageService.GetSelectedEmrConnectionId();
                if (connectionId.HasValue)
                {
                    request.Headers.Add(EmrConnectionIdHeaderName, connectionId.Value.ToString());
                }
            }
        }

        public async Task<T> GetAsync<T>(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            await AddEmrConnectionHeaderIfNeeded(request);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>(_jsonOptions);
        }

        public async Task<T> PostAsync<T>(string url, object data)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(data, _jsonOptions),
                Encoding.UTF8,
                "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = content;
            await AddEmrConnectionHeaderIfNeeded(request);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>(_jsonOptions);
        }

        public async Task PutAsync(string url, object data)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(data, _jsonOptions),
                Encoding.UTF8,
                "application/json");

            var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Content = content;
            await AddEmrConnectionHeaderIfNeeded(request);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url);
            await AddEmrConnectionHeaderIfNeeded(request);

            var response = await _httpClient.SendAsync(request); // Corrected: send the HttpRequestMessage
            response.EnsureSuccessStatusCode();
        }
    }
}
