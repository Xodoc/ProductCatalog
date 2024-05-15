using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.Api.Contracts
{
    public class CreateUserRequest
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; } = string.Empty;
    }
}
