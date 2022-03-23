using Application.Interfaces;
using MassTransit;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application
{
    public class CurrencyApplication : ICurrencyApplication
    {
        private readonly IPublisherApplication<CreateCurrencyIntegrationEvent> publisherApplication;
        private readonly IPublishEndpoint publisher;

        public CurrencyApplication(IPublisherApplication<CreateCurrencyIntegrationEvent> publisherApplication, IPublishEndpoint publisher)
        {
            this.publisherApplication = publisherApplication;
            this.publisher = publisher;
        }

        public async Task<ResultWrapper> SendCurrency(CurrencyRequestModel currencyRequest, Guid requestId, CancellationToken ctx)
        {
            var currency = new CreateCurrencyIntegrationEvent(requestId, currencyRequest.Name, currencyRequest.Description);
           
            await publisher.Publish<ICreateCurrencyIntegrationEvent>(currency, ctx);

            var response = new CurrencyResponseModel
            {
                CurrencyId = requestId
            };
            return ResultWrapper.Accepted(response);
        }
    }
}
