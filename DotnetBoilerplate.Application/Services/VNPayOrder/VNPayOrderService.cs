using AutoMapper;
using DotnetBoilerplate.Application.Dtos;
using DotnetBoilerplate.Application.Repositories;
using DotnetBoilerplate.Application.Services.CurrentUser;
using DotnetBoilerplate.Application.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Collections.Specialized;

namespace DotnetBoilerplate.Application.Services.VNPayOrder
{
    public class VNPayOrderService : IVNPayOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly ICurrentUserService _currentUserService;
        private readonly string _vnp_Url;
        private readonly string _vnp_HashSecret;
        private readonly string _vnp_TmnCode;

        public VNPayOrderService(
            IOrderRepository orderRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor,
            ICurrentUserService currentUserService)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _currentUserService = currentUserService;
            _vnp_Url = _configuration.GetSection("VNPaySettings:VnPayUrl").Value;
            _vnp_HashSecret = _configuration.GetSection("VNPaySettings:VnPayHashSecret").Value;
            _vnp_TmnCode = _configuration.GetSection("VNPaySettings:VnPayTmnCode").Value;
        }

        public async Task<PaymentResponse> CreateOrderWithPaymentUrl(PaymentRequest paymentRequest)
        {
            Domain.Entities.Order order = _mapper.Map<Domain.Entities.Order>(paymentRequest);
            order.CustomerName = paymentRequest.CustomerName;
            order.CustomerPhone = paymentRequest.CustomerPhone;
            order.PaymentMethodId = (int)Domain.Enums.PaymentMethodEnum.VNPay;
            order.UserId = _currentUserService.UserId;
            await _orderRepository.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();

            var vnPayLibrary = new VnPayLibrary();

            vnPayLibrary.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnPayLibrary.AddRequestData("vnp_Command", "pay");
            vnPayLibrary.AddRequestData("vnp_TmnCode", _vnp_TmnCode);
            vnPayLibrary.AddRequestData("vnp_Amount", (paymentRequest.Amount * 100).ToString());
            vnPayLibrary.AddRequestData("vnp_CurrCode", paymentRequest.Currency);
            vnPayLibrary.AddRequestData("vnp_TxnRef", order.Id.ToString());
            vnPayLibrary.AddRequestData("vnp_OrderInfo", "Order" + order.Id);
            vnPayLibrary.AddRequestData("vnp_ReturnUrl", paymentRequest.ReturnUrl);
            vnPayLibrary.AddRequestData("vnp_IpAddr", PayLibUtils.GetIpAddress(_httpContextAccessor.HttpContext));
            vnPayLibrary.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnPayLibrary.AddRequestData("vnp_Locale", paymentRequest.Locale);
            vnPayLibrary.AddRequestData("vnp_OrderType", paymentRequest.OrderType);

            string paymentUrl = vnPayLibrary.CreateRequestUrl(_vnp_Url, _vnp_HashSecret);

            return new PaymentResponse
            {
                OrderId = order.Id,
                PaymentUrl = paymentUrl,
            };
        }

        public async Task<VNPayPaymentNotificationResponse> ConfirmPayment(NameValueCollection queryString)
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
                    var orderId = int.Parse(vnPayLibrary.GetResponseData("vnp_TxnRef"));
                    var order = await _orderRepository.GetByIdAsync(orderId);
                    if (order.Status == Domain.Enums.OrderStatusEnum.WaitingForPayment)
                    {
                        order.Status = Domain.Enums.OrderStatusEnum.Success;
                        order.PaymentOrderId = vnPayLibrary.GetResponseData("vnp_TransactionNo");
                        _orderRepository.Update(order);
                        await _unitOfWork.SaveChangesAsync();
                    }
                    return new VNPayPaymentNotificationResponse
                    {
                        RspCode = "00",
                        Message = "Success"
                    };
                }
                else
                {
                    onFailure(int.Parse(vnPayLibrary.GetResponseData("vnp_TxnRef")));
                    return new VNPayPaymentNotificationResponse
                    {
                        RspCode = vnp_ResponseCode,
                        Message = "Transaction Failed"
                    };
                }
            }
            else
            {
                onFailure(int.Parse(vnPayLibrary.GetResponseData("vnp_TxnRef")));
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

        private async Task onFailure(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order.Status != Domain.Enums.OrderStatusEnum.WaitingForPayment) return;
            order.Status = Domain.Enums.OrderStatusEnum.Failure;
            _orderRepository.Update(order);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
