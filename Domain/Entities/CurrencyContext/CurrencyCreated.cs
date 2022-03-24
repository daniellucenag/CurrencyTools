using System;

namespace Domain.Entities.CurrencyContext
{
    public struct CurrencyCreated
    {
        public CurrencyCreated(Guid currencyId)
        {
            CurrencyId = currencyId;
        }

        public Guid CurrencyId { get; }
    }
}
