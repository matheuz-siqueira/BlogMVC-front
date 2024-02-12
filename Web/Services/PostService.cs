using System.Net;
using System.Text.Json;
using Web.Models.Post;
using Web.Services.Contracts;

namespace Web.Services;

public class PostService : IPostService
{
    private readonly IHttpClientFactory _httpClientFactory;
    const string endpoint = "/api/post/";
    private readonly JsonSerializerOptions _options; 
    private  IEnumerable<GetPostsViewModel> posts; 
    public PostService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory; 
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true }; 
    }
    public async Task<IEnumerable<GetPostsViewModel>> GetAll()
    {
        var client = _httpClientFactory.CreateClient("BlogAPI"); 
        using var response = await client.GetAsync(endpoint);
        if(response.IsSuccessStatusCode && response.StatusCode != HttpStatusCode.NoContent)
        {
            var apiResponse = await response.Content.ReadAsStreamAsync(); 
            posts = await JsonSerializer
                    .DeserializeAsync<IEnumerable<GetPostsViewModel>>
                        (apiResponse, _options);
        } 
        else 
        {
            return null; 
        }
        return posts; 
    }
}
