using Microsoft.AspNetCore.Mvc;
using Web.Models.Post;
using Web.Services.Contracts;

namespace Web.Controllers;

public class PostController : Controller
{
    private readonly IPostService _postService;
    public PostController(IPostService postService)
    {
        _postService = postService; 
    }
    public async Task<ActionResult<IEnumerable<GetPostsViewModel>>> Index()
    {
        var response = await _postService.GetAll(); 
        if(response is null)
        {
            return View("Error"); 
        }
        return View(response); 
    }
}
