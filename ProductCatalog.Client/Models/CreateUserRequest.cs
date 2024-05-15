using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.Client.Models
{
    public class CreateUserRequest
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }
    }
}
