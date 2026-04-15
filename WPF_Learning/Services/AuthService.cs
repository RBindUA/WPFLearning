using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace WPF_Learning.Services
{
    public class AuthService
    {
        private readonly HttpClient _client = new HttpClient();
        /*PORT  
          * Order https://localhost:52635
          * User https://localhost:55646
          * Identity https://localhost:55648 
          */
        private readonly string _authUrl = "https://localhost:55648/api/auth/login";

        public async Task<bool> LoginAsync(string username, string password)
        {
            var loginRequest = new { Email = username, Password = password };

            try
            {
                var response = await _client.PostAsJsonAsync(_authUrl, loginRequest);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

                    if (result != null)
                    {
                        UserSession.Token = result.Token;
                        UserSession.BusinessEntityID = result.BusinessEntityID;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                //better to log after testing is done
                return false;
            }

            return false;
        }
    }

    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public int BusinessEntityID { get; set; }
        public DateTime Expiration { get; set; }  // Future auto-logout
    }
}
