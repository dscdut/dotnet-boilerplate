using DotnetBoilerplate.Domain.Common;
using DotnetBoilerplate.Domain.Enums;

namespace DotnetBoilerplate.Domain.Entities
{
    public class Order : IntegerIDTrackable
    {
        public string CustomerName { get; set; } = null!;
        public string CustomerPhone { get; set; } = null!;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = null!;
        public OrderStatusEnum Status { get; set; } = OrderStatusEnum.WaitingForPayment;
    }
}
