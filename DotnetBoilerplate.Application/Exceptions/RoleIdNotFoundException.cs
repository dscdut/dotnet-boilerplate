namespace DotnetBoilerplate.Application.Exceptions
{
    public class RoleIdNotFoundException: NotFoundException
    {
        public RoleIdNotFoundException() : base("The role ID does not exist")
        {
        }
    }
}
