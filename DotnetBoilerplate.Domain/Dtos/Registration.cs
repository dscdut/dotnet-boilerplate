using System.ComponentModel.DataAnnotations;

namespace DotnetBoilerplate.Domain.Dtos
{
    public class Registration
    {

        [Required(ErrorMessage = "Name is required")]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 150 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Email must be between 6 and 50 characters")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$",
        ErrorMessage = "Password must be at least 8 characters long and contain at least one letter, one number, and one special character.")]
        public string? Password { get; set; }

        public int? Role { get; set; }
    }
}
