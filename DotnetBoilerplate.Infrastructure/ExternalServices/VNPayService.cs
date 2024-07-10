using DotnetBoilerplate.Application.Dtos;
using DotnetBoilerplate.Application.ExternalServices;
using DotnetBoilerplate.Domain.Entities;
using DotnetBoilerplate.Infrastructure.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Collections.Specialized;

namespace DotnetBoilerplate.Infrastructure.ExternalServices
{
    public class VNPayService : IPaymentService
    {
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private string _vnp_Url;
        private string _vnp_HashSecret;
        private string _vnp_TmnCode;

        public VNPayService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _vnp_Url = _configuration.GetSection("VNPaySettings:VnPayUrl").Value;
            _vnp_HashSecret = _configuration.GetSection("VNPaySettings:VnPayHashSecret").Value;
            _vnp_TmnCode = _configuration.GetSection("VNPaySettings:VnPayTmnCode").Value;
        }

        public PaymentResponse ProcessPayment(PaymentRequest paymentRequest, Order order)
        {
            var vnPayLibrary = new VnPayLibrary();

            vnPayLibrary.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnPayLibrary.AddRequestData("vnp_Command", "pay");
            vnPayLibrary.AddRequestData("vnp_TmnCode", _vnp_TmnCode);
            vnPayLibrary.AddRequestData("vnp_Amount", (paymentRequest.Amount * 100).ToString());
            vnPayLibrary.AddRequestData("vnp_CurrCode", paymentRequest.Currency);
            vnPayLibrary.AddRequestData("vnp_TxnRef", order.Id.ToString());
            vnPayLibrary.AddRequestData("vnp_OrderInfo", paymentRequest.OrderInfo);
            vnPayLibrary.AddRequestData("vnp_ReturnUrl", paymentRequest.ReturnUrl);
            vnPayLibrary.AddRequestData("vnp_IpAddr", PayLibUtils.GetIpAddress(_httpContextAccessor.HttpContext));
            vnPayLibrary.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnPayLibrary.AddRequestData("vnp_Locale", paymentRequest.Locale);
            vnPayLibrary.AddRequestData("vnp_OrderType", paymentRequest.OrderType);

            string paymentUrl = vnPayLibrary.CreateRequestUrl(_vnp_Url, _vnp_HashSecret);

            return new PaymentResponse
            {
                PaymentUrl = paymentUrl,
            };
        }

        public async Task<IPaymentNotificationResponse> HandlePaymentNotification(NameValueCollection queryString, Func<int, Task>? onSuccess, Func<int, Task>? onFailure)
        {
            var vnPayLibrary = new VnPayLibrary();
            foreach (string key in queryString.AllKeys)
            {
                vnPayLibrary.AddResponseData(key, queryString[key]);
            }
            string vnp_SecureHash = vnPayLibrary.GetResponseData("vnp_SecureHash");
            bool isValidSignature = vnPayLibrary.ValidateSignature(vnp_SecureHash, _vnp_HashSecret);

            if (isValidSignature)
            {
                string vnp_ResponseCode = vnPayLibrary.GetResponseData("vnp_ResponseCode");
                if (vnp_ResponseCode == "00")
                {
                    if (onSuccess != null)
                        await onSuccess(int.Parse(vnPayLibrary.GetResponseData("vnp_TxnRef")));
                    return new VNPayPaymentNotificationResponse
                    {
                        RspCode = "00",
                        Message = "Success"
                    };
                }
                else
                {
                    if (onFailure != null)
                        await onFailure(int.Parse(vnPayLibrary.GetResponseData("vnp_TxnRef")));
                    return new VNPayPaymentNotificationResponse
                    {
                        RspCode = vnp_ResponseCode,
                        Message = "Transaction Failed"
                    };
                }
            }
            else
            {
                return new VNPayPaymentNotificationResponse
                {
                    RspCode = "97",
                    Message = "Invalid Signature"
                };
            }
        }

        public VerifyPaymentResponse VerifyPaymentResponse(NameValueCollection queryString)
        {
            var vnPayLibrary = new VnPayLibrary();
            foreach (string key in queryString.AllKeys)
            {
                vnPayLibrary.AddResponseData(key, queryString[key]);
            }
            string vnp_SecureHash = vnPayLibrary.GetResponseData("vnp_SecureHash");
            bool isValidSignature = vnPayLibrary.ValidateSignature(vnp_SecureHash, _vnp_HashSecret);
            string vnp_ResponseCode = vnPayLibrary.GetResponseData("vnp_ResponseCode");
            if (isValidSignature && vnp_ResponseCode == "00")
            {
                return new VerifyPaymentResponse
                {
                    Message = "Success"
                };
            }
            else
            {
                return new VerifyPaymentResponse
                {
                    Message = "Failed"
                };
            }
        }
    }
}