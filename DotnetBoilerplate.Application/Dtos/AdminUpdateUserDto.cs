using System.Text.Json.Serialization;

namespace DotnetBoilerplate.Application.Dtos
{
    public class AdminUpdateUserDto
    {
        [JsonPropertyName("full_name")]
        public string? FullName { get; set; }
        public string? Email { get; set; }
        [JsonPropertyName("role_id")]
        public int? RoleId { get; set; }
    }
}
