namespace DotnetBoilerplate.Domain.Dtos
{
    public class CreateUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
