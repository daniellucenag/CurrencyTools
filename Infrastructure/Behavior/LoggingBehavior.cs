
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Behavior
{
    [ExcludeFromCodeCoverage]
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> logger;

        public Guid Id { get; }

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            this.logger = logger;
            this.Id = Guid.NewGuid();
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var requestTypeName = request?.GetType().FullName;
            var requestJson = JsonSerializer.Serialize(request);
            logger.LogInformation("----- Id: {Id} - Handling Request {RequestName} ({@Request})", Id, requestTypeName, requestJson);

            try
            {
                var response = await next();
                var responseType = response?.GetType() ?? typeof(TResponse);
                var responseJson = JsonSerializer.Serialize(response, responseType);
                logger.LogInformation("-----Id: {Id} - Handled {RequestName} handled - response: {@Response}", Id, requestTypeName, responseJson);
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Id: {Id} - Unhandled Exception response", Id);
                throw;
            }
        }
    }
}
