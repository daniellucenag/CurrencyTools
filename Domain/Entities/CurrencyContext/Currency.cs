using Domain.Core.Entities;
using Domain.Events;
using Flunt.Validations;
using System;

namespace Domain.Entities.CurrencyContext
{
    public class Currency : RootEntity, IAggregateRoot
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public string CurrencyApiCode { get; protected set; }

        public Currency(string name, string description, string currencyApiCode, Guid? identifid = null)
        {
            Name = name;
            Description = description;
            CurrencyApiCode = currencyApiCode;
            Id = identifid ?? Id;

            AddNotifications(new Contract()
              .Requires()
              .IsNotNullOrEmpty(Name, nameof(Name), $"{nameof(Name)} can't be null or empty")
              .IsNotNullOrEmpty(Description, nameof(Description), $"{nameof(Description)} can't be null or empty")
            );
        }

        protected Currency()
        { }

        public void CurrencyCreatedEvent()
        {
            var currencyCreated = new CurrencyCreated(GetCurrencyId());
            var @event = new CurrencyCreatedEvent(currencyCreated);
            AddDomainEvent(@event);
        }

        private Guid GetCurrencyId()
        {
            return Id;
        }
    }
}