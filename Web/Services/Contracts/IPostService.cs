using Web.Models.Post;

namespace Web.Services.Contracts;

public interface IPostService
{
    Task<IEnumerable<GetPostsViewModel>> GetAll(); 
    Task<GetPostViewModel> CreateAsync(CreatePostViewModel model, string token);
}
