using System.Linq.Expressions;
using DotnetBoilerplate.Domain.Entities;

namespace DotnetBoilerplate.Domain.Specifications.Users;

public class UserIdSpecification(int id) : Specification<User>
{
    public override Expression<Func<User, bool>> ToExpression()
    {
        return user => user.Id == id;
    }
}