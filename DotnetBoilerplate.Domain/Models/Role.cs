using DotnetBoilerplate.Domain.Common;

namespace DotnetBoilerplate.Domain.Models
{
    public class Role : BaseDomainEntity
    {
        public string? Name { get; set; }
        public List<User> Users { get; set; }
    }
}
