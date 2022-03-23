using Domain.Core;
using System;

namespace Application
{
    public class CreateCurrencyIntegrationEvent : ICreateCurrencyIntegrationEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public CreateCurrencyIntegrationEvent(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
            CreatedAt = DateTime.Now;
        }
    }

    public interface ICreateCurrencyIntegrationEvent
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        DateTimeOffset CreatedAt { get; set; }
    }
}