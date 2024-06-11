using System.Linq.Expressions;
using DotnetBoilerplate.Domain.Entities;
using DotnetBoilerplate.Domain.Enums;

namespace DotnetBoilerplate.Domain.Specifications.Users;

public class UserRoleIdSpecification : Specification<User>
{
    private readonly RoleEnum _roleEnum;

    public UserRoleIdSpecification(RoleEnum roleEnum)
    {
        _roleEnum = roleEnum;
    }

    public override Expression<Func<User, bool>> ToExpression()
    {
        return user => user.RoleId == (int)_roleEnum;
    }
}