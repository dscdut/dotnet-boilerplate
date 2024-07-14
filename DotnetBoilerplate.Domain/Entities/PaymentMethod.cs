using DotnetBoilerplate.Domain.Common;

namespace DotnetBoilerplate.Domain.Entities
{
    public class PaymentMethod : IntegerIDTrackable
    {
        public string Name { get; set; }
    }
}
