using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using WPF_Learning.Models;
using System.Text.Json;

namespace WPF_Learning.Services
{
    public class OrderService
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly string _orderUrl = "https://localhost:52635/api/orders";

        public async Task<List<OrderHistoryDTO>?> GetUserOrdersAsync()
        {
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", UserSession.Token);

            try
            {
                string fullUrl = $"{_orderUrl}/user/{UserSession.BusinessEntityID}";
                return await _client.GetFromJsonAsync<List<OrderHistoryDTO>>(fullUrl);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Detailed Error: {ex.Message} \n\n Inner: {ex.InnerException?.Message}");
                return null;
            }
        }
        public async Task<bool> SubmitOrderAsync(OrderDTO order)
        {
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", UserSession.Token);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            try
            {
                var response = await _client.PostAsJsonAsync(_orderUrl, order, options);

                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    System.Windows.MessageBox.Show($"Server Error: {response.StatusCode}\n{errorContent}");
                }

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Order Submission Failed: {ex.Message}");
                return false;
            }
        }
    }
}