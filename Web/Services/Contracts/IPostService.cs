using Web.Models.Post;

namespace Web.Services.Contracts;

public interface IPostService
{
    Task<IEnumerable<GetPostsViewModel>> GetAll(); 
}
