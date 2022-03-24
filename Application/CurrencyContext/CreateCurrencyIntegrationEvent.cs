using System;

namespace Application.CurrencyContext
{
    public class CreateCurrencyIntegrationEvent : ICreateCurrencyIntegrationEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CurrencyApiCode { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public CreateCurrencyIntegrationEvent(Guid id, string name, string description, string currencyApiCode)
        {
            Id = id;
            Name = name;
            Description = description;
            CurrencyApiCode = currencyApiCode;
            CreatedAt = DateTime.Now;
        }
    }

    public interface ICreateCurrencyIntegrationEvent
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string CurrencyApiCode { get; set; }
        DateTimeOffset CreatedAt { get; set; }
    }
}