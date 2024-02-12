using Web.Models.User;

namespace Web.Services.Contracts;

public interface IAuthenticateService
{
    Task<TokenViewModel> LoginAsync(AuthViewModel model); 
}
