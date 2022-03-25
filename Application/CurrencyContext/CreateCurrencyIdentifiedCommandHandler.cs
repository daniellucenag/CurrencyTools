using Application.Interfaces;
using AutoMapper;
using Domain.Core.SeedWork;
using Domain.Entities.CurrencyContext;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CurrencyContext
{
    public class CreateCurrencyIdentifiedCommandHandler : IdentifiedCommandHandler<CreateCurrencyCommand, ResultWrapper>
    {
        private readonly IMapper mapper;
        private readonly IRepository<Currency> currencyRepository;

        public CreateCurrencyIdentifiedCommandHandler(
            IMediator mediator,
            IRequestManager requestManager,
            IMapper mapper,
            IRepository<Currency> currencyRepository
            ) : base(mediator, requestManager)
        {
            this.mapper = mapper;
            this.currencyRepository = currencyRepository;
        }

        public override async Task<ResultWrapper> CreateResultForRequest(IdentifiedCommand<CreateCurrencyCommand, ResultWrapper> request,
            CancellationToken cancellationToken)
        {
            var currency = mapper.Map<Currency>(request.Command.CreateCurrencyIntegrationEvent);

            if (currency.Invalid)
                return ResultWrapper.Error(currency.Notifications);

            await currencyRepository.Add(currency);

            currency.CurrencyCreatedEvent();

            return ResultWrapper.Created(currency);
        }

        public override ResultWrapper CreateResultForDuplicateRequest()
        {
            return ResultWrapper.Conflict();
        }
    }
}
