using System.Text;
using System.Text.Json;
using Web.Models.User;
using Web.Services.Contracts;

namespace Web.Services;

public class AuthenticateService : IAuthenticateService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly JsonSerializerOptions _options;
    const string endpoint = "/api/login/";
    private TokenViewModel tokenResponse; 
    public AuthenticateService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory; 
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true }; 
    }
    public async Task<TokenViewModel> LoginAsync(AuthViewModel model)
    {
        var client = _httpClientFactory.CreateClient("BlogAPI"); 
        var request = JsonSerializer.Serialize(model); 
        StringContent content = new(request, Encoding.UTF8, "application/json"); 
        using var response = await client.PostAsync(endpoint, content); 
        if(response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadAsStreamAsync(); 
            tokenResponse = await JsonSerializer
                    .DeserializeAsync<TokenViewModel>
                        (apiResponse, _options);
        }
        else 
        {
            return null; 
        }
        return tokenResponse; 
    }
}
