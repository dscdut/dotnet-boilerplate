using DotnetBoilerplate.Application.Dtos;
using System.Collections.Specialized;

namespace DotnetBoilerplate.Application.ExternalServices
{
    public interface IPaymentService
    {
        PaymentResponse ProcessPayment(PaymentRequest request);
        IPaymentNotificationResponse HandlePaymentNotification(NameValueCollection queryString);
    }

}
