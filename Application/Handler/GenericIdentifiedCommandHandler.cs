using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Application.Handler
{
    [ExcludeFromCodeCoverage]
    public class GenericIdentifiedCommandHandler<TRequest, TData>
        where TData : struct
        where TRequest : IntegrationEvent<TData>, IRequest<ResultWrapper>

    {

        private readonly IMediator mediator;
        private readonly ILogger<GenericIdentifiedCommandHandler<TRequest, TData>> logger;

        /// <summary>
        /// Constructor Generic RequestHandler
        /// </summary>
        /// <param name="mediator">IMediator</param>
        /// <param name="logger">ILogger GenericRequestHandler</param>
        public GenericIdentifiedCommandHandler(IMediator mediator, ILogger<GenericIdentifiedCommandHandler<TRequest, TData>> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        /// <summary>
        /// Function Handle
        /// </summary>
        /// <param name="model">TRequest</param>
        /// <param name="cancellationToken">CancellationToken</param>
        public virtual async Task Handle(TRequest model, System.Threading.CancellationToken cancellationToken)
        {
            try
            {
                var request = new IdentifiedCommand<TRequest, ResultWrapper>(model, model.Id);
                await mediator.Send(request, cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Date: {date} - Unhandled Exception mediator send", DateTime.Now);
                throw;
            }
        }
    }
}
