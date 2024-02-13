using System.ComponentModel.DataAnnotations;

namespace Web.Models.Post;

public class CreatePostViewModel
{
    [Required(ErrorMessage = "{0} is required")]
    [StringLength(120, MinimumLength = 5, ErrorMessage = "{0} size should be between {2} and {1}")]
    public string Title { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [StringLength(120, MinimumLength = 5, ErrorMessage = "{0} size should be between {2} and {1}")]
    public string Subtitle { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    public string Content { get; set; } 
}
