using Application.Currency;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Application
{
	public class CreateCurrencyConsumer : IConsumer<ICreateCurrencyIntegrationEvent>
	{ 
		private readonly IMediator mediator;
		private readonly ILogger<CreateCurrencyConsumer> logger;

        public CreateCurrencyConsumer(ILogger<CreateCurrencyConsumer> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        public async Task Consume(ConsumeContext<ICreateCurrencyIntegrationEvent> context)
        {
			try
			{
				logger.LogInformation("Date: {date} - Message {messageId} received: {context}", DateTime.Now, context.Message.Id, context.Message);

				var command = new CreateCurrencyCommand(context.Message);
				var request = new IdentifiedCommand<CreateCurrencyCommand, ResultWrapper>(command, context.Message.Id);
				var commandResult = await mediator.Send(request);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Date: {date} - Unhandled Exception mediator send", DateTime.Now);
				throw;
			}
		}
	}
}