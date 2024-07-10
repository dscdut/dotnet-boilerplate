using DotnetBoilerplate.Api.Utils;
using DotnetBoilerplate.Application.Dtos;
using DotnetBoilerplate.Application.ExternalServices;
using DotnetBoilerplate.Application.Services.Order;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;

namespace DotnetBoilerplate.Api.Controllers
{
    [ApiController]
    [Route("payment")]
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;
        private IHttpContextAccessor _httpContextAccessor;

        public OrderController(IOrderService orderService, IHttpContextAccessor httpContextAccessor)
        {
            _orderService = orderService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("process")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequest request)
        {
            var response = await _orderService.CreateOrderWithPaymentUrl(request);
            return Ok(response);
        }

        [HttpGet]
        [HttpPost]
        [Route("confirm/{provider}")]
        public async Task<IActionResult> HandleNotification(string provider)
        {
            var queryParamsCollection = RequestUtils.GetAllQueryParams(_httpContextAccessor);
            var response = await _orderService.ConfirmPayment(queryParamsCollection, provider);
            return Ok(response);
        }

        [HttpGet("verify/{provider}")]
        public IActionResult VerifyPaymentResponse(string provider)
        {
            var queryParamsCollection = RequestUtils.GetAllQueryParams(_httpContextAccessor);
            var response = _orderService.VerifyPaymentResponse(queryParamsCollection, provider);
            return Ok(response);
        }
    }
}
