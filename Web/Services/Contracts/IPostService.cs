using Web.Models.Post;

namespace Web.Services.Contracts;

public interface IPostService
{
    Task<IEnumerable<GetPostsViewModel>> GetAll(); 
    Task<GetPostViewModel> CreateAsync(CreatePostViewModel model, string token);
    Task<IEnumerable<GetPostsViewModel>> GetAllUser(string token);
    Task<GetPostViewModel> DetailsAsynct(string token, int id);
    Task<GetPostViewModel> GetByIdAsync(int id, string token); 
    Task<bool> UpdateAsync(int id, CreatePostViewModel model, string token); 
}
