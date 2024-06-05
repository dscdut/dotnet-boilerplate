using DotnetBoilerplate.Application.Dtos;

namespace DotnetBoilerplate.Application.ExternalServices;

public interface IEmailService
{
    Task SendMailAsync(MailDataDto mailData);
    Task SendEmailForgotPassword(string email);
}