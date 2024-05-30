using System.ComponentModel.DataAnnotations;
namespace DotnetBoilerplate.Domain.Dtos
{
    public class TokenObtainPair
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MinLength(1, ErrorMessage = "Email must be at least 1 character")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(1, ErrorMessage = "Password must be at least 1 character")]
        public string? Password { get; set; }
    }
}
