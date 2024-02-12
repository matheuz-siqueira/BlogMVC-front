using System.ComponentModel.DataAnnotations;

namespace Web.Models.User;

public class AuthViewModel
{
    [Required(ErrorMessage = "{0} is required")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    
    [Required(ErrorMessage = "{0} is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
