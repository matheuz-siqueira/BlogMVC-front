using Microsoft.AspNetCore.Mvc;
using Web.Models.User;
using Web.Services.Contracts;

namespace Web.Controllers;

public class UserController : Controller
{
    private readonly IAccountService _accountService;
    private readonly IAuthenticateService _authenticateService;
    public UserController(IAccountService accountService, 
        IAuthenticateService authenticateService)
    {
        _accountService = accountService; 
        _authenticateService = authenticateService; 
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Login(AuthViewModel model)
    {
        if(!ModelState.IsValid)
        {
            return View(model); 
        }
        var response = await _authenticateService.LoginAsync(model); 
        if(response is null)
        {
            return View("Error");    
        }
        Response.Cookies.Append("access-token", response.Token, new CookieOptions()
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict
        });
        return Redirect("/");
    }

    [HttpGet]
    public IActionResult CreateAccount()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> CreateAccount(UserViewModel model)
    {
        if(!ModelState.IsValid)
        {   
            ModelState.AddModelError(string.Empty, "Invalid request..."); 
            return View(); 
        }
        var response = await _accountService.CreateAccountAsync(model);
        if(response is null)
        {
            return View("Error"); 
        } 
        return RedirectToAction(nameof(Login)); 

    }
}
