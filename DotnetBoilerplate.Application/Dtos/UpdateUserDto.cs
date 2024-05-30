using System.Text.Json.Serialization;

namespace DotnetBoilerplate.Application.Dtos
{
    public class UpdateUserDto
    {
        [JsonPropertyName("full_name")]
        public string? FullName { get; set; }
        public string? Email { get; set; }
    }
}
