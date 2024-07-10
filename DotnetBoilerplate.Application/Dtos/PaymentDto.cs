using System.Text.Json.Serialization;

namespace DotnetBoilerplate.Application.Dtos
{
    public class PaymentRequest
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "VND";
        public string OrderInfo { get; set; }
        public string ReturnUrl { get; set; }
        public string OrderType { get; set; } = "other";
        public string Locale { get; set; } = "vn";
        public string Provider { get; set; } = "VNPay";
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

    public class VerifyPaymentResponse
    {
        public string Message { get; set; }
    }
}
