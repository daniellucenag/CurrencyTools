using Domain.Events;
using Infrastructure.Utils;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Publisher
{
    [ExcludeFromCodeCoverage]
    public class DomainEventHandler : IDomainEventHandler
    {
        private readonly List<INotification> domainEvents = new List<INotification>();
        private readonly IMediator mediator;
        private readonly ILogger<DomainEventHandler> logger;
        private static readonly Type genericDomainEvent = typeof(IDomainEvent<>);

        public DomainEventHandler(IMediator mediator, ILogger<DomainEventHandler> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }
        public void AddDomainEvent(params INotification[] notifications)
        {
            domainEvents.AddRange(notifications);
        }

        public async Task Handler()
        {
            var events = domainEvents.ToList();
            domainEvents.Clear();

            foreach (var domainEvent in events)
            {
                await mediator.Publish(domainEvent);
                var typeDomain = domainEvent.GetType();
                if (genericDomainEvent.IsInstanceOfGenericType(typeDomain))
                {
                    var serialize = JsonSerializer.Serialize(domainEvent, typeDomain);
                    logger.LogInformation("Domain Event {DomainName} handled - response: {Event}", domainEvent.GetType().Name, serialize);
                }
            }

            if (domainEvents.Any())
                await Handler();
        }
    }
}
