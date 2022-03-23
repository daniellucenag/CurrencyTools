using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Currency
{
    public class CreateCurrencyCommand : IRequest<ResultWrapper>
    {
        public CreateCurrencyCommand(ICreateCurrencyIntegrationEvent createCurrencyIntegrationEvent)
        {
            CreateCurrencyIntegrationEvent = createCurrencyIntegrationEvent;
        }

        public ICreateCurrencyIntegrationEvent CreateCurrencyIntegrationEvent { get; set; }
    }
}