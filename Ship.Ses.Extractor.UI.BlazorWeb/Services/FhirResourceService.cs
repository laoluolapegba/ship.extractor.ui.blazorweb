using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using global::Ship.Ses.Extractor.UI.BlazorWeb.Models.ApiClient;

namespace Ship.Ses.Extractor.UI.BlazorWeb.Services
{
   
    public class FhirResourceService
    {
        private readonly ApiClientService _apiClient;

        public FhirResourceService(ApiClientService apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<List<FhirResourceTypeModel>> GetResourceTypesAsync()
        {
            return await _apiClient.GetAsync<List<FhirResourceTypeModel>>("mappings/resource-types");
        }

        public async Task<FhirResourceTypeModel> GetResourceTypeAsync(int id)
        {
            return await _apiClient.GetAsync<FhirResourceTypeModel>($"mappings/resource-types/{id}");
        }

        public async Task<JsonDocument> GetResourceStructureAsync(int resourceTypeId)
        {
            //return await _apiClient.GetAsync<string>($"mappings/resource-types/{resourceTypeId}/structure");
            return await _apiClient.GetAsync<JsonDocument>($"mappings/resource-types/{resourceTypeId}/structure");

        }
    }

}
