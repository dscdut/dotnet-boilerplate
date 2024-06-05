using DotnetBoilerplate.Application.Dtos;

namespace DotnetBoilerplate.Application.ExternalServices;

public interface IEmailService
{
    Task SendMailAsync(MailData mailData);
    Task SendEmailForgotPassword(string email);
}