﻿using DotnetBoilerplate.Domain.Common;

namespace DotnetBoilerplate.Domain.Models
{
    public class User : BaseDomainEntity
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public DateTime? LastLogin { get; set; }
        public bool IsSuperUser { get; set; }
        public bool IsStaff { get; set; }
        public bool IsActive { get; set; }

        public DateTime DateJoined { get; set; } = DateTime.UtcNow;
        public int RoleId { get; set; }
        public Role? Role { get; set; }
    }
}
