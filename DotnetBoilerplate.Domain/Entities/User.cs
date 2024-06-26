using DotnetBoilerplate.Domain.Common;

namespace DotnetBoilerplate.Domain.Entities
{
    public class User : IntegerIDTrackable
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public DateTime? LastLogin { get; set; }
        public bool IsSuperUser { get; set; }
        public bool IsStaff { get; set; }
        public bool IsActive { get; set; }

        public DateTime DateJoined { get; set; } = DateTime.UtcNow;
        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;
    }
}
