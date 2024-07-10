using DotnetBoilerplate.Application.Dtos;
using System.Collections.Specialized;

namespace DotnetBoilerplate.Application.Services.Order
{
    public interface IOrderService
    {
        Task<PaymentResponse> CreateOrderWithPaymentUrl(PaymentRequest paymentRequest);
        Task<IPaymentNotificationResponse> ConfirmPayment(NameValueCollection queryString, string provider);
        VerifyPaymentResponse VerifyPaymentResponse(NameValueCollection queryString, string provider);
    }
}
