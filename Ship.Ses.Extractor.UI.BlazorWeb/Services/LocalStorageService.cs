using Blazored.LocalStorage;
using Microsoft.JSInterop;
using System.Threading.Tasks;
namespace Ship.Ses.Extractor.UI.BlazorWeb.Services
{


    public class LocalStorageService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IJSRuntime _jsRuntime;
        private const string SelectedEmrConnectionKey = "SelectedEmrConnectionId";

        //public LocalStorageService(ILocalStorageService localStorage)
        //{
        //    _localStorage = localStorage;
        //}
        public LocalStorageService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }
        public async Task<T> GetItemAsync<T>(string key)
        {
            return await _localStorage.GetItemAsync<T>(key);
        }

        public async Task SetItemAsync<T>(string key, T value)
        {
            await _localStorage.SetItemAsync(key, value);
        }

        public async Task RemoveItemAsync(string key)
        {
            await _localStorage.RemoveItemAsync(key);
        }
        /// <summary>
        /// Sets the selected EMR connection ID in local storage.
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public async Task SetSelectedEmrConnectionId(int connectionId)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", SelectedEmrConnectionKey, connectionId.ToString());
        }

        public async Task<int?> GetSelectedEmrConnectionId()
        {
            var idString = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", SelectedEmrConnectionKey);
            if (int.TryParse(idString, out int id))
            {
                return id;
            }
            return null;
        }

        public async Task RemoveSelectedEmrConnectionId()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", SelectedEmrConnectionKey);
        }
    }
}
