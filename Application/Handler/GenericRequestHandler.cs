using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handler
{
	[ExcludeFromCodeCoverage]
	public class GenericRequestHandler<TRequest> where TRequest : IRequest<ResultWrapper>
	{
		private readonly IMediator mediator;
		private readonly ILogger<GenericRequestHandler<TRequest>> logger;

		public GenericRequestHandler(IMediator mediator, ILogger<GenericRequestHandler<TRequest>> logger)
		{
			this.mediator = mediator;
			this.logger = logger;
		}
		public virtual async Task Handle(TRequest model, System.Threading.CancellationToken cancellationToken)
		{
			try
			{
				await mediator.Send(model, cancellationToken);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Date: {date} - Unhandled Exception mediator send", DateTime.Now);
				throw;
			}
		}
	}
}
