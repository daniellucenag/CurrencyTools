using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Publisher
{
    public class PublisherRabbitMq<TRequest> : IPublisherApplication<TRequest>
    {  
        private readonly ILogger<PublisherRabbitMq<TRequest>> logger;
        private readonly IPublishEndpoint _publisher;

        public PublisherRabbitMq()
        {

        }
    }
}
