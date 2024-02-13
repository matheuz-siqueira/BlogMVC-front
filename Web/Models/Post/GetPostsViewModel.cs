using System.ComponentModel.DataAnnotations;

namespace Web.Models.Post;

public class GetPostsViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Subtitle { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime UpdateAt { get; set; }
}
