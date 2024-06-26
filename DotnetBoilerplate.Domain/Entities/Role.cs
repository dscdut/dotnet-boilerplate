using DotnetBoilerplate.Domain.Common;

namespace DotnetBoilerplate.Domain.Entities
{
    public class Role : IntegerIDTrackable
    {
        public string Name { get; set; } = null!;
        public ICollection<User> Users { get; set; } = [];
    }
}
