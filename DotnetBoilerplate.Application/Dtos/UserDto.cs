using System.Text.Json.Serialization;

namespace DotnetBoilerplate.Application.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }

        [JsonPropertyName("full_name")]
        public string? FullName { get; set; }

        public string? Email { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public RoleDto? Role { get; set; }
    }
}
