using Application.Interfaces;
using MassTransit;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CurrencyContext
{
    public class CurrencyApplication : ICurrencyApplication
    {
        private readonly IPublishEndpoint publisher;

        public CurrencyApplication(IPublishEndpoint publisher)
        {
            this.publisher = publisher;
        }

        public async Task<ResultWrapper> SendCurrency(CurrencyRequestModel currencyRequest, Guid requestId, CancellationToken ctx)
        {
            var currency = new CreateCurrencyIntegrationEvent(requestId, currencyRequest.Name, currencyRequest.Description, currencyRequest.CurrencyApiCode);

            await publisher.Publish<ICreateCurrencyIntegrationEvent>(currency, ctx);

            var response = new CurrencyResponseModel
            {
                CurrencyId = requestId
            };
            return ResultWrapper.Accepted(response);
        }
    }
}
