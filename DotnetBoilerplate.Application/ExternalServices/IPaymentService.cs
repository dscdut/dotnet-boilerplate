using DotnetBoilerplate.Application.Dtos;
using DotnetBoilerplate.Domain.Entities;
using System.Collections.Specialized;

namespace DotnetBoilerplate.Application.ExternalServices
{
    public interface IPaymentService
    {
        PaymentResponse ProcessPayment(PaymentRequest request, Order order);
        Task<IPaymentNotificationResponse> HandlePaymentNotification(NameValueCollection queryString, Func<int, Task>? onSuccess = null, Func<int, Task>? onFailure = null);

        VerifyPaymentResponse VerifyPaymentResponse(NameValueCollection queryString);
    }

}
