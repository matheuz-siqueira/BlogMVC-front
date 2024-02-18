using Microsoft.AspNetCore.Mvc;
using Web.Models.Post;
using Web.Services.Contracts;

namespace Web.Controllers;

public class PostController : Controller
{
    private readonly IPostService _postService;
    private string token = string.Empty;
    public PostController(IPostService postService)
    {
        _postService = postService; 
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetPostsViewModel>>> Index()
    {
        var response = await _postService.GetAll(); 
        if(response is null)
        {
            return View("Error"); 
        }
        return View(response); 
    }

    [HttpGet]
    public IActionResult NewPost()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult<GetPostViewModel>> NewPost(CreatePostViewModel model)
    {
        if(!ModelState.IsValid)
        {
            return View("Error"); 
        }
        var response = await _postService.CreateAsync(model, GetToken()); 
        if(response is null)
        {
            return View("Error"); 
        }
        return RedirectToAction(nameof(Index)); 
    }

    [HttpGet]
    public async Task<ActionResult<GetPostsViewModel>> MyPosts()
    {
        var response = await _postService.GetAllUser(GetToken()); 
        if(response is null)
        {
            return View("Error"); 
        }
        return View(response); 
    }

    [HttpGet]
    public async Task<ActionResult> Details(int id) 
    {
        var response = await _postService.DetailsAsynct(GetToken(), id); 
        if(response is null)
        {
            return View("Error"); 
        }
        return View(response); 
    }

    [HttpGet]
    public async Task<ActionResult> Edit(int id)
    {
        var response = await _postService.GetByIdAsync(id, GetToken()); 
        if(response is null)
        {
            return View("Error"); 
        }
        return View(response);      
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, CreatePostViewModel model)
    {
        if(!ModelState.IsValid)
        {
            return View();
        }
        var response = await _postService.UpdateAsync(id, model, GetToken()); 
        if(!response)
            return View("Error"); 
        return RedirectToAction(nameof(MyPosts)); 
    }

    private string GetToken()
    {
        if(HttpContext.Request.Cookies.ContainsKey("access-token"))
            token = HttpContext.Request.Cookies["access-token"].ToString(); 
        return token; 
    }
}
