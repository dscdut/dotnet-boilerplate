using DotnetBoilerplate.Domain.Common;

namespace DotnetBoilerplate.Domain.Entities
{
    public class Role : BaseDomainEntity
    {
        public string? Name { get; set; }
        public List<User>? Users { get; set; }
    }
}
