using System.Linq.Expressions;
using DotnetBoilerplate.Domain.Entities;

namespace DotnetBoilerplate.Domain.Specifications.Users;

public class UserEmailSpecification : Specification<User>
{
    private readonly string _email;

    public UserEmailSpecification(string email)
    {
        _email = email;
    }

    public override Expression<Func<User, bool>> ToExpression()
    {
        return user => user.Email == _email;
    }
}