namespace Web.Models.Post;

public class GetPostViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Content { get; set; } 
    public DateTime UpdateAt { get; set; }
}
