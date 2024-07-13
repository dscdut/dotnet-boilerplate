using DotnetBoilerplate.Api.Utils;
using DotnetBoilerplate.Application.Dtos;
using DotnetBoilerplate.Application.Services.VNPayOrder;
using Microsoft.AspNetCore.Mvc;

namespace DotnetBoilerplate.Api.Controllers
{
    [ApiController]
    [Route("vnpay")]
    public class VNPayOrderController : ControllerBase
    {
        private IVNPayOrderService _orderService;
        private IHttpContextAccessor _httpContextAccessor;

        public VNPayOrderController(IVNPayOrderService orderService, IHttpContextAccessor httpContextAccessor)
        {
            _orderService = orderService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("create-payment-url")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequest request)
        {
            var response = await _orderService.CreateOrderWithPaymentUrl(request);
            return Ok(response);
        }

        [HttpGet]
        [Route("confirm")]
        public async Task<IActionResult> HandleNotification()
        {
            var queryParamsCollection = RequestUtils.GetAllQueryParams(_httpContextAccessor);
            var response = await _orderService.ConfirmPayment(queryParamsCollection);
            return Ok(response);
        }

        [HttpGet("verify")]
        public IActionResult VerifyPaymentResponse()
        {
            var queryParamsCollection = RequestUtils.GetAllQueryParams(_httpContextAccessor);
            var response = _orderService.VerifyPaymentResponse(queryParamsCollection);
            return Ok(response);
        }
    }
}
