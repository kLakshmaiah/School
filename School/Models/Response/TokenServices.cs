using Newtonsoft.Json;
using School.Models.IRepository;
using System.Text;

namespace School.Models.Response
{
    public class TokenServices : ITokenServices
    {
        private readonly HttpClient _HttpClient;

        public TokenServices(HttpClient httpClient)
        {
            _HttpClient = httpClient;

        }
        async Task<Token> ITokenServices.GetTokens(Login login)
        {
            var courseJson = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var response = await _HttpClient.PostAsync($"api/Authentication", courseJson);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<Token>(await response.Content.ReadAsStringAsync());
            }
            return null;
        }
    }
}
