using MediatR;

namespace Application.CurrencyContext
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