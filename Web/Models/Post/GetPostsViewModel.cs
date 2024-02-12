using System.ComponentModel.DataAnnotations;

namespace Web.Models.Post;

public class GetPostsViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public DateTime UpdateAt { get; set; }
}
