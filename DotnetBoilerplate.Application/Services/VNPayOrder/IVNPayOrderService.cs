using DotnetBoilerplate.Application.Dtos;
using System.Collections.Specialized;

namespace DotnetBoilerplate.Application.Services.VNPayOrder
{
    public interface IVNPayOrderService
    {
        Task<PaymentResponse> CreateOrderWithPaymentUrl(PaymentRequest paymentRequest);
        Task<VNPayPaymentNotificationResponse> ConfirmPayment(NameValueCollection queryString);
        VerifyPaymentResponse VerifyPaymentResponse(NameValueCollection queryString);
    }
}
