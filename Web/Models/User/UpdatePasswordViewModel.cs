using System.ComponentModel.DataAnnotations;

namespace Web.Models.User;

public class UpdatePasswordViewModel
{
    [Required(ErrorMessage = "{0} is required")]
    public string CurrentPassword { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    public string NewPassword { get; set; }
}
