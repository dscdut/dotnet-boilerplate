using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using DotnetBoilerplate.Application.ExternalServices;
using DotnetBoilerplate.Application.Dtos;
using DotnetBoilerplate.Application.Exceptions;
using Microsoft.AspNetCore.Http;

namespace DotnetBoilerplate.Infrastructure.ExternalServices
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    
        public Task SendEmailForgotPassword(string email)
        {
            throw new NotImplementedException();
        }
    
        public async Task SendMailAsync(MailDataDto mailData)
        {
            try
            {
                using (var emailMessage = new MimeMessage())
                {
                    var fromEmailAddress = _configuration.GetSection("SMTPConfigs:Email").Value;
                    var fromEmailDisplayName = _configuration.GetSection("SMTPConfigs:Displayname").Value;
                    var fromEmailPassword = _configuration.GetSection("SMTPConfigs:Password").Value;
                    var smtpHost = _configuration.GetSection("SMTPConfigs:Host").Value;
                    var smtpPort = _configuration.GetValue<int>("SMTPConfigs:Port");

                    var emailFrom = new MailboxAddress(fromEmailDisplayName, fromEmailAddress);
                    emailMessage.From.Add(emailFrom);
                    var emailTo = new MailboxAddress(mailData.EmailToName, mailData.EmailToId);
                    emailMessage.To.Add(emailTo);

                    // you can add the CCs and BCCs here.
                    //emailMessage.Cc.Add(new MailboxAddress("Cc Receiver", "cc@example.com"));
                    //emailMessage.Bcc.Add(new MailboxAddress("Bcc Receiver", "bcc@example.com"));

                    emailMessage.Subject = mailData.EmailSubject;

                    var emailBodyBuilder = new BodyBuilder();
                    emailBodyBuilder.TextBody = mailData.EmailBody;

                    emailMessage.Body = emailBodyBuilder.ToMessageBody();
                    //this is the SmtpClient from the Mailkit.Net.Smtp namespace, not the System.Net.Mail one
                    using (var mailClient = new SmtpClient())
                    {
                        await mailClient.ConnectAsync(smtpHost, smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                        await mailClient.AuthenticateAsync(fromEmailAddress, fromEmailPassword);
                        await mailClient.SendAsync(emailMessage);
                        await mailClient.DisconnectAsync(true);
                    }
                }
            }
            catch (Exception)
            {
                throw new CustomException(StatusCodes.Status500InternalServerError, "Mail Service Error");
            }
        }
    }

}
