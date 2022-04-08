using System.ComponentModel.DataAnnotations;

namespace ContactMe.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Not specified Email")]
    public string Email { get; set; }
         
    [Required(ErrorMessage = "Not specified password")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Display(Name = "RememberMe?")]
    public bool RememberMe { get; set; }
}