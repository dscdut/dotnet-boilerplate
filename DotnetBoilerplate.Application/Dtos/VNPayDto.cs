using System.Text.Json.Serialization;

namespace DotnetBoilerplate.Application.Dtos
{
    public class PaymentRequest
    {
        [JsonPropertyName("customer_name")]
        public string CustomerName { get; set; }
        [JsonPropertyName("customer_phone")]
        public string CustomerPhone { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "VND";
        [JsonPropertyName("return_url")]
        public string ReturnUrl { get; set; }
        [JsonPropertyName("order_type")]
        public string OrderType { get; set; } = "other";
        public string Locale { get; set; } = "vn";
    }

    public class PaymentResponse
    {
        [JsonPropertyName("order_id")]
        public int OrderId { get; set; }

        [JsonPropertyName("payment_url")]
        public string PaymentUrl { get; set; }
    }

    public class VNPayPaymentNotificationResponse 
    {
        public string RspCode { get; set; }
        public string Message { get; set; }
    }

    public class VerifyPaymentResponse
    {
        public string Message { get; set; }
    }
}
