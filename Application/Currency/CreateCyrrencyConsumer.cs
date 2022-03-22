using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Currency
{
    public class CreateCurrencyConsumer : IConsumer<CreateCurrencyIntegrationEvent>
    {
        private readonly ILogger<CreateCurrencyConsumer> _logger;

        public CreateCurrencyConsumer(ILogger<CreateCurrencyConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<CreateCurrencyIntegrationEvent> context)
        { 
            throw new NotImplementedException();
        }
    }
}
