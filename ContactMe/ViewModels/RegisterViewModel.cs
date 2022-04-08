using System.ComponentModel.DataAnnotations;

namespace ContactMe.ViewModels
{

    public class RegisterViewModel
    {
        [Required] 
        [Display(Name = "FirstName")] 
        public string FirstName { get; set; }
        
        [Required] 
        [Display(Name = "LastName")] 
        public string LastName { get; set; }
        
        [Required] 
        [Display(Name = "Email")] 
        public string Email { get; set; }
        
        [Required] 
        [Display(Name = "Age")] 
        public int Age { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords doesn't match!")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string? PasswordConfirm { get; set; }
    }
}