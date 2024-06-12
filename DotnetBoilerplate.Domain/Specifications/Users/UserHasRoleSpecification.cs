using System.Linq.Expressions;
using DotnetBoilerplate.Domain.Entities;
using DotnetBoilerplate.Domain.Enums;

namespace DotnetBoilerplate.Domain.Specifications.Users;

public class UserHasRoleSpecification : Specification<User>
{
    private readonly List<int> _roleIds;

    public UserHasRoleSpecification(IEnumerable<RoleEnum> roles)
    {
        _roleIds = roles.Select(role => (int)role).ToList();
    }

    public override Expression<Func<User, bool>> ToExpression()
    {
        return user => _roleIds.Contains(user.RoleId);
    }
}