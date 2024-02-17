using Web.Models.User;

namespace Web.Services.Contracts;

public interface IAccountService
{
    Task<TokenViewModel> CreateAccountAsync(UserViewModel model);
    Task<UserViewModel> GetProfileAsync(string token); 
}
