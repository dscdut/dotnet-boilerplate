﻿using DotnetBoilerplate.Domain.Common;
using DotnetBoilerplate.Domain.Models;

namespace DotnetBoilerplate.Domain.Dtos
{
    public class UserDto : BaseDomainEntity
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        //public string? Password { get; set; }

        //public DateTime? LastLogin { get; set; }
        //public bool IsSuperUser { get; set; }
        //public bool IsStaff { get; set; }
        //public bool IsActive { get; set; }

        //public DateTime DateJoined { get; set; }
        //public int RoleId { get; set; }
        public RoleDto? Role { get; set; }
    }
}
