using Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application
{
    public class CurrencyApplication : ICurrencyApplication
    {
        private readonly IPublisherApplication<CreateCurrencyIntegrationEvent> publisherApplication;

        public CurrencyApplication(IPublisherApplication<CreateCurrencyIntegrationEvent> publisherApplication)
        {
            this.publisherApplication = publisherApplication;
        }

        public async Task<ResultWrapper> SendCurrency(CurrencyRequestModel currencyRequest, Guid requestId, CancellationToken ctx)
        {
            var currency = new CreateCurrency(requestId, currencyRequest.Name, currencyRequest.Description);
            var integrationEvent = new CreateCurrencyIntegrationEvent(currency);
            
            await publisherApplication.Publish(integrationEvent, requestId, ctx);

            var response = new CurrencyResponseModel
            {
                CurrencyId = requestId
            };
            return ResultWrapper.Accepted(response);
        }
    }
}
