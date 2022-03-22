using Application;
using Application.Interfaces;
using Domain.Core;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Publisher
{
    public class PublisherRabbitMq<TRequest> : IPublisherApplication<TRequest>
    {
        private readonly ILogger<PublisherRabbitMq<TRequest>> logger;
        private readonly IPublishEndpoint publisher;

        public PublisherRabbitMq(ILogger<PublisherRabbitMq<TRequest>> logger, IPublishEndpoint publisher)
        {
            this.logger = logger;
            this.publisher = publisher;
        }

        public async Task Publish(TRequest request, Guid requestId, CancellationToken ctx)
        {
            var requestTypeName = request?.GetType().FullName;
            var requestJson = JsonSerializer.Serialize(request);
            logger.LogInformation("----- Handling Publiser Id: {publisherId} - {RequestName} ({@Request})", requestId, requestTypeName, requestJson);
            await publisher.Publish(request, ctx);
            logger.LogInformation("----- Handled Publiser Id: {publisherId} - {RequestName})", requestId, requestTypeName);
        }
    }


}