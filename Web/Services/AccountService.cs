using System.Text;
using System.Text.Json;
using Web.Models.User;
using Web.Services.Contracts;
using Web.Utils;

namespace Web.Services;

public class AccountService : IAccountService
{
    private readonly IHttpClientFactory _httpClientFactory; 
    const string endpoint = "/api/users/"; 
    private readonly JsonSerializerOptions _options; 
    private readonly HeaderAuthorization _headerAuth;
    private TokenViewModel tokenResponse;
    private UserViewModel userVM; 
    public AccountService(IHttpClientFactory httpClientFactory, 
        HeaderAuthorization headerAuth)
    {
        _httpClientFactory = httpClientFactory;
        _headerAuth = headerAuth; 
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

    public async Task<UserViewModel> GetProfileAsync(string token)
    {
        var client = _httpClientFactory.CreateClient("BlogAPI");
        _headerAuth.PutTokenInHeaderAuthorization(token, client); 
        using var response = await client.GetAsync(endpoint); 
        if(response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadAsStreamAsync(); 
            userVM = await JsonSerializer
                .DeserializeAsync<UserViewModel>
                    (apiResponse, _options);    
        }
        else
        {
            return null; 
        }
        return userVM;
    }
}
