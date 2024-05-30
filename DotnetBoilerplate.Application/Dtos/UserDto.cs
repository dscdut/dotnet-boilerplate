using DotnetBoilerplate.Domain.Common;
using System.Text.Json.Serialization;

namespace DotnetBoilerplate.Application.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }

        [JsonPropertyName("full_name")]
        public string? FullName { get; set; }

        public string? Email { get; set; }
        //public string? Password { get; set; }

        //public DateTime? LastLogin { get; set; }
        //public bool IsSuperUser { get; set; }
        //public bool IsStaff { get; set; }
        //public bool IsActive { get; set; }

        //public DateTime DateJoined { get; set; }
        //public int RoleId { get; set; }
        //public RoleDto? Role { get; set; }
    }
}
