using WPF_Learning.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;

namespace WPF_Learning.Services
{
    public class ProductServiceClient
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7147/api/Products";

        public ProductServiceClient(HttpClient httpClient)
        {
        _httpClient = httpClient;
        }

        public async Task<List<ProductDTO>> GetCatalogAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<ProductDTO>>(BaseUrl) ?? new();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load catalog{ex.Message}","Connection error",MessageBoxButton.OK);
                return new List<ProductDTO>();
            }
        }
    }
}
