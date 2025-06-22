using System.Collections.Generic;
using System.Threading.Tasks;
using global::Ship.Ses.Extractor.UI.BlazorWeb.Models.ApiClient;

namespace Ship.Ses.Extractor.UI.BlazorWeb.Services
{

    public class EmrDatabaseService // Assuming this is now the UI-side service
    {
        private readonly ApiClientService _apiClient;
        private readonly LocalStorageService _localStorageService;
        private int? _selectedConnectionId; // To keep track of the selected ID in memory

        public EmrDatabaseService(ApiClientService apiClient, LocalStorageService localStorageService)
        {
            _apiClient = apiClient;
            _localStorageService = localStorageService;
        }

        // --- EMR Connection Management ---

        public async Task<List<EmrConnectionModel>> GetConnectionsAsync()
        {
            return await _apiClient.GetAsync<List<EmrConnectionModel>>("emr/connections");
        }

        public async Task<EmrConnectionModel> GetConnectionByIdAsync(int id)
        {
            return await _apiClient.GetAsync<EmrConnectionModel>($"emr/connections/{id}");
        }

        public async Task<int> CreateConnectionAsync(EmrConnectionModel connection)
        {
            // API expects EmrConnectionDto, which EmrConnectionModel should be mappable to.
            // If they are identical, you can directly pass it. Otherwise, create a DTO.
            return await _apiClient.PostAsync<int>("emr/connections", connection);
        }

        public async Task UpdateConnectionAsync(EmrConnectionModel connection)
        {
            await _apiClient.PutAsync($"emr/connections/{connection.Id}", connection);
        }

        public async Task DeleteConnectionAsync(int id)
        {
            await _apiClient.DeleteAsync($"emr/connections/{id}");
        }

        public async Task<bool> TestConnectionAsync(int connectionId)
        {
            try
            {
                // This hits the new endpoint on the API which handles the selection internally
                await _apiClient.PostAsync<object>($"emr/connections/test/{connectionId}", new { }); // Send empty object for POST
                return true;
            }
            catch (HttpRequestException ex)
            {
                // Log the exception if you have a UI-side logger
                Console.WriteLine($"Error testing connection: {ex.Message}");
                return false;
            }
        }

        public async Task SelectConnectionAsync(int connectionId)
        {
            await _localStorageService.SetSelectedEmrConnectionId(connectionId);
            _selectedConnectionId = connectionId; // Update in-memory cache
            // You might want to optionally call the API's select endpoint here,
            // but the header injection in ApiClientService will handle it for subsequent calls.
            // The API's /connections/select/{id} endpoint is mainly for direct API-driven selection
            // or if you want an immediate server-side validation/acknowledgement of selection.
            // For now, storing in local storage and sending header is sufficient.
        }

        public async Task<int?> GetSelectedConnectionIdAsync()
        {
            if (!_selectedConnectionId.HasValue)
            {
                _selectedConnectionId = await _localStorageService.GetSelectedEmrConnectionId();
            }
            return _selectedConnectionId;
        }

        public async Task ClearSelectedConnectionAsync()
        {
            await _localStorageService.RemoveSelectedEmrConnectionId();
            _selectedConnectionId = null;
        }


        // --- EMR Database Schema (These will now implicitly use the selected connection via ApiClientService's header) ---

        public async Task<List<EmrTableModel>> GetEmrTablesAsync()
        {
            return await _apiClient.GetAsync<List<EmrTableModel>>("emr/tables");
        }

        public async Task<EmrTableModel> GetEmrTableSchemaAsync(string tableName)
        {
            return await _apiClient.GetAsync<EmrTableModel>($"emr/tables/{tableName}");
        }
    }
}
