namespace DotnetBoilerplate.Domain.Enums
{
    public enum RoleEnum
    {
        Admin = 1,
        Member = 2
    }

    public enum ErrorCodeEnum
    {
        InvalidRequest = 101,
        IncorrectEmailOrPassword = 102,
        ServerError = 103
    }
}
