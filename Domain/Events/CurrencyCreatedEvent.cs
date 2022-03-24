using Domain.Entities.CurrencyContext;

namespace Domain.Events
{
    public class CurrencyCreatedEvent : DomainEventBase<CurrencyCreated>
    {
        public CurrencyCreatedEvent(CurrencyCreated currencyCreated) : base(currencyCreated)
        {
        }
    }
}
