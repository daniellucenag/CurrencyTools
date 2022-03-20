using Datadog.Trace;
using MediatR;
using Serilog.Context;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Behavior
{
    [ExcludeFromCodeCoverage]
    public class TracingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var requestTypeName = request?.GetType().Name;
            using (var scope = Tracer.Instance.StartActive(requestTypeName))
            using (LogContext.PushProperty("dd_env", CorrelationIdentifier.Env))
            using (LogContext.PushProperty("dd_service", CorrelationIdentifier.Service))
            using (LogContext.PushProperty("dd_version", CorrelationIdentifier.Version))
            using (LogContext.PushProperty("dd_trace_id", CorrelationIdentifier.TraceId.ToString()))
            using (LogContext.PushProperty("dd_span_id", CorrelationIdentifier.SpanId.ToString()))
            {
                try
                {
                    return await next();
                }
                catch (System.Exception ex)
                {
                    scope.Span.SetException(ex);
                    throw;
                }
            }
        }
    }
}
