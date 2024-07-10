using DotnetBoilerplate.Application.Dtos;
using DotnetBoilerplate.Application.ExternalServices;
using DotnetBoilerplate.Infrastructure.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;

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

        public PaymentResponse ProcessPayment(PaymentRequest paymentRequest)
        {
            var vnPayLibrary = new VnPayLibrary();

            vnPayLibrary.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnPayLibrary.AddRequestData("vnp_Command", "pay");
            vnPayLibrary.AddRequestData("vnp_TmnCode", _vnp_TmnCode);
            vnPayLibrary.AddRequestData("vnp_Amount", (paymentRequest.Amount * 100).ToString());
            vnPayLibrary.AddRequestData("vnp_CurrCode", paymentRequest.Currency);
            vnPayLibrary.AddRequestData("vnp_TxnRef", paymentRequest.OrderId);
            vnPayLibrary.AddRequestData("vnp_OrderInfo", paymentRequest.OrderInfo);
            vnPayLibrary.AddRequestData("vnp_ReturnUrl", paymentRequest.ReturnUrl);
            vnPayLibrary.AddRequestData("vnp_IpAddr", Utils.Utils.GetIpAddress(_httpContextAccessor.HttpContext));
            vnPayLibrary.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnPayLibrary.AddRequestData("vnp_Locale", "vn");
            vnPayLibrary.AddRequestData("vnp_OrderType", "other");

            string paymentUrl = vnPayLibrary.CreateRequestUrl(_vnp_Url, _vnp_HashSecret);

            return new PaymentResponse
            {
                PaymentUrl = paymentUrl,
            };
        }

        public VNPayPaymentNotificationResponse HandlePaymentNotification(NameValueCollection queryString)
        {
            var vnPayLibrary = new VnPayLibrary();
            foreach (string key in queryString.AllKeys)
            {
                vnPayLibrary.AddResponseData(key, queryString[key]);
            }
            string vnp_SecureHash = queryString["vnp_SecureHash"];
            bool isValidSignature = vnPayLibrary.ValidateSignature(vnp_SecureHash, _vnp_HashSecret);

            if (isValidSignature)
            {
                string vnp_ResponseCode = queryString["vnp_ResponseCode"];
                if (vnp_ResponseCode == "00")
                {
                    return new VNPayPaymentNotificationResponse
                    {
                        RspCode = "00",
                        Message = "Success"
                    };
                }
                else
                {
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
    }

    public class RandomAlphanumeric
    {
        private static readonly char[] chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

        public static string GenerateRandomAlphanumeric(int length)
        {
            var random = new Random();
            var stringBuilder = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                int randomIndex = random.Next(chars.Length);
                stringBuilder.Append(chars[randomIndex]);
            }

            return stringBuilder.ToString();
        }
    }
}