using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Web.Models.Post;
using Web.Services.Contracts;

namespace Web.Services;

public class PostService : IPostService
{
    private readonly IHttpClientFactory _httpClientFactory;
    const string endpoint = "/api/post/";
    private readonly JsonSerializerOptions _options; 
    private IEnumerable<GetPostsViewModel> posts;
    private GetPostViewModel post; 
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

    public async Task<GetPostViewModel> CreateAsync(CreatePostViewModel model, string token)
    {
        var client = _httpClientFactory.CreateClient("BlogAPI"); 
        PutTokenInHeaderAuthorization(token, client); 
        var postModel = JsonSerializer.Serialize(model); 
        StringContent content = new StringContent(postModel, Encoding.UTF8, "application/json"); 
        using var response = await client.PostAsync(endpoint, content); 
        if(response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadAsStreamAsync(); 
            post = await JsonSerializer
                .DeserializeAsync<GetPostViewModel>
                    (apiResponse, _options);
        }
        else 
        {
            return null; 
        }
        return post; 
    }

    public async Task<IEnumerable<GetPostsViewModel>> GetAllUser(string token)
    {
        var client = _httpClientFactory.CreateClient("BlogAPI"); 
        PutTokenInHeaderAuthorization(token, client); 
        using var response = await client.GetAsync(endpoint + "all-user/"); 
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

    public async Task<GetPostViewModel> DetailsAsynct(string token, int id)
    {
        var client = _httpClientFactory.CreateClient("BlogAPI");
        PutTokenInHeaderAuthorization(token, client); 
        using var response = await client.GetAsync(endpoint + id); 
        if(response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadAsStreamAsync(); 
            post = await JsonSerializer
                .DeserializeAsync<GetPostViewModel>
                    (apiResponse, _options);
        }   
        else 
        {
            return null; 
        } 
        return post; 
    }
    public async Task<GetPostViewModel> GetByIdAsync(int id, string token)
    {
        var client = _httpClientFactory.CreateClient("BlogAPI"); 
        PutTokenInHeaderAuthorization(token, client); 
        using var response = await client.GetAsync(endpoint + id); 
        if(response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadAsStreamAsync(); 
            post = await JsonSerializer
                .DeserializeAsync<GetPostViewModel>
                    (apiResponse, _options); 
        }
        else 
        {
            return null; 
        }
        return post; 
    }

    public async Task<bool> UpdateAsync(int id, CreatePostViewModel model, string token)
    {
        var client = _httpClientFactory.CreateClient("BlogAPI"); 
        PutTokenInHeaderAuthorization(token, client); 
        using var response = await client.PutAsJsonAsync(endpoint + id, model); 
        if(response.IsSuccessStatusCode)
            return true; 
        else 
            return false; 
    }

    private static void PutTokenInHeaderAuthorization(string token, HttpClient client)
    {
        client.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", token);
    }
}
