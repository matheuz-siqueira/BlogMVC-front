using System.Text;
using System.Text.Json;
using Web.Models.User;
using Web.Services.Contracts;

namespace Web.Services;

public class AccountService : IAccountService
{
    private readonly IHttpClientFactory _httpClientFactory; 
    const string endpoint = "/api/users/"; 
    private readonly JsonSerializerOptions _options; 
    private TokenViewModel tokenResponse;
    public AccountService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true }; 
    } 
    public async Task<TokenViewModel> CreateAccountAsync(UserViewModel model)
    {
        var client = _httpClientFactory.CreateClient("BlogAPI"); 
        var user = JsonSerializer.Serialize(model); 
        StringContent content = new(user, Encoding.UTF8, "application/json"); 
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
