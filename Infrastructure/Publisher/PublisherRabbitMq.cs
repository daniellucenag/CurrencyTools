using Application.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Publisher
{
    public class PublisherRabbitMq<TMessage> : IPublisherApplication<TMessage> where TMessage : class
    {
        private readonly ILogger<PublisherRabbitMq<TMessage>> logger;
        private readonly IPublishEndpoint publisher;

        public PublisherRabbitMq(ILogger<PublisherRabbitMq<TMessage>> logger, IPublishEndpoint publisher)
        {
            this.logger = logger;
            this.publisher = publisher;
        }

        public async Task Publish(TMessage request, Guid requestId, CancellationToken ctx)
        {
            var requestTypeName = request?.GetType().FullName;
            var requestJson = JsonSerializer.Serialize(request);
            logger.LogInformation("----- Handling Publiser Id: {publisherId} - {RequestName} ({@Request})", requestId, requestTypeName, requestJson);
            await publisher.Publish(request, ctx);
            logger.LogInformation("----- Handled Publiser Id: {publisherId} - {RequestName})", requestId, requestTypeName);
        }
    }
}