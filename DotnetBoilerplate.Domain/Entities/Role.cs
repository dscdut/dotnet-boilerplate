using DotnetBoilerplate.Domain.Common;

namespace DotnetBoilerplate.Domain.Entities
{
    public class Role : BaseDomainEntity
    {
        public string Name { get; set; } = null!;
        public List<User> Users { get; set; } = [];
    }
}
