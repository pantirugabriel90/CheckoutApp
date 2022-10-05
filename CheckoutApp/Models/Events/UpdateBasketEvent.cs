
using CheckoutApp.Models.Domain;

namespace CheckoutApp.Models
{
    public class UpdateBasketEvent : IEvent
    {
        public Status Status { get; set; }
        public long EntityId { get; set; }
    }
}
