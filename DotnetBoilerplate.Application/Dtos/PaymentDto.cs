using System.Text.Json.Serialization;

namespace DotnetBoilerplate.Application.Dtos
{
    public class PaymentRequest
    {
        public string OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string ReturnUrl { get; set; }
        public string OrderInfo { get; set; }
    }

    public class PaymentResponse
    {
        [JsonPropertyName("payment_url")]
        public string PaymentUrl { get; set; }
    }

    public interface IPaymentNotificationResponse { }

    public class VNPayPaymentNotificationResponse : IPaymentNotificationResponse
    {
        public string RspCode { get; set; }
        public string Message { get; set; }
    }


}
