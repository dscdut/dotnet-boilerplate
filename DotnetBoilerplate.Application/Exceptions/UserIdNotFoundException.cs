namespace DotnetBoilerplate.Application.Exceptions
{
    public class UserIdNotFoundException: NotFoundException
    {
        public UserIdNotFoundException() : base("The user ID does not exist")
        {
        }
    }
}
