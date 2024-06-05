using System.Text.Json.Serialization;
namespace DotnetBoilerplate.Application.Dtos
{
    public class RegistrationDto
    {

        [JsonPropertyName("full_name")]
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        [JsonPropertyName("confirm_password")]
        public string? ConfirmPassword { get; set; }
    }
}
