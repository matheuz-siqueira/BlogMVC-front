using Microsoft.AspNetCore.Mvc;
using Web.Models.User;
using Web.Services.Contracts;

namespace Web.Controllers;

public class UserController : Controller
{
    private readonly IAccountService _accountService;
    public UserController(IAccountService accountService)
    {
        _accountService = accountService; 
    }
    public IActionResult Index()
    {
        return View();
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
        return RedirectToAction(nameof(Index)); 

    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
}
