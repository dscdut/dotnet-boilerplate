using DotnetBoilerplate.Application.Dtos;
using DotnetBoilerplate.Application.ExternalServices;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetBoilerplate.Infrastructure.ExternalServices
{
    public class MoMoService : IPaymentService
    {
        public PaymentNotificationResponse HandlePaymentNotification(NameValueCollection queryString)
        {
            throw new NotImplementedException();
        }

        public PaymentResponse ProcessPayment(PaymentRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
