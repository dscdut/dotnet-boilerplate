using DotnetBoilerplate.Application.Dtos;
using DotnetBoilerplate.Application.ExternalServices;
using DotnetBoilerplate.Application.Repositories;
using DotnetBoilerplate.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;

namespace DotnetBoilerplate.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class PaymentController : ControllerBase
    {
        private readonly Func<string, IPaymentService> _paymentServiceFactory;

        public PaymentController(Func<string, IPaymentService> paymentServiceFactory)
        {
            _paymentServiceFactory = paymentServiceFactory;
        }

        [HttpPost("process")]
        public IActionResult ProcessPayment([FromBody] PaymentRequest request, string provider)
        {
            var paymentService = _paymentServiceFactory(provider);
            var response = paymentService.ProcessPayment(request);
            return Ok(response);
        }

        [HttpGet("notify/{provider}")]
        public async Task<IActionResult> HandleNotification([FromQuery] NameValueCollection queryString, string provider)
        {
            var paymentService = _paymentServiceFactory(provider);
            var response = paymentService.HandlePaymentNotification(queryString);
            return Ok(response);
        }
    }
}
