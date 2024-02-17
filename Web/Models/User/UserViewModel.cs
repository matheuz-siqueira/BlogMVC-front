using System.ComponentModel.DataAnnotations;

namespace Web.Models.User;

public class UserViewModel
{
    [Required(ErrorMessage =  "{0} is required")]
    [StringLength(30, MinimumLength = 3)]
    public string Name { get; set; }
    [Required(ErrorMessage = "{0} is requerid")]
    [StringLength(30, MinimumLength = 3)]
    public string Surname { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    [StringLength(250)]
    public string Email { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string Password { get; set; }
}
