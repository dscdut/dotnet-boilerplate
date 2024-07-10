using AutoMapper;
using DotnetBoilerplate.Application.Dtos;
using DotnetBoilerplate.Application.ExternalServices;
using DotnetBoilerplate.Application.Repositories;
using System.Collections.Specialized;

namespace DotnetBoilerplate.Application.Services.Order
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _orderRepository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private Func<string, IPaymentService> _paymentServiceFactory;

        public OrderService(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IMapper mapper, Func<string, IPaymentService> paymentServiceFactory)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _paymentServiceFactory = paymentServiceFactory;
        }

        public async Task<PaymentResponse> CreateOrderWithPaymentUrl(PaymentRequest paymentRequest)
        {
            Domain.Entities.Order order = _mapper.Map<Domain.Entities.Order>(paymentRequest);
            order.CustomerName = "Test Customer"; // Hardcoded for now, should be fetched from user's session or JWT token
            order.CustomerPhone = "0123456789"; // Hardcoded for now, should be fetched from user's session or JWT token
            await _orderRepository.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();
            var paymentService = _paymentServiceFactory(paymentRequest.Provider);
            return paymentService.ProcessPayment(paymentRequest, order);
        }

        public async Task<IPaymentNotificationResponse> ConfirmPayment(NameValueCollection queryString, string provider)
        {
            var paymentService = _paymentServiceFactory(provider);
            return await paymentService.HandlePaymentNotification(queryString, onSuccess(), onFailure());
        }

        public VerifyPaymentResponse VerifyPaymentResponse(NameValueCollection queryString, string provider)
        {
            var paymentService = _paymentServiceFactory(provider);
            return paymentService.VerifyPaymentResponse(queryString);
        }
        private Func<int, Task> onSuccess() => async (orderId) =>
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order.Status != Domain.Enums.OrderStatusEnum.WaitingForPayment) return;
            order.Status = Domain.Enums.OrderStatusEnum.Success;
            _orderRepository.Update(order);
            await _unitOfWork.SaveChangesAsync();
        };

        private Func<int, Task> onFailure() => async (orderId) =>
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order.Status != Domain.Enums.OrderStatusEnum.WaitingForPayment) return;
            order.Status = Domain.Enums.OrderStatusEnum.Failure;
            _orderRepository.Update(order);
            await _unitOfWork.SaveChangesAsync();
        };
    }
}
