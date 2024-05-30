using System.ComponentModel.DataAnnotations;
namespace DotnetBoilerplate.Application.Dtos
{
    public class TokenObtainPairDto
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
