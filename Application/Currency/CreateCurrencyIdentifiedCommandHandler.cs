using Application.Interfaces;
using AutoMapper;
using Domain.Core.SeedWork;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Currency
{
	public class CreateCurrencyIdentifiedCommandHandler : IdentifiedCommandHandler<CreateCurrencyCommand, ResultWrapper>
	{
		private readonly IMapper mapper;
		private readonly IRepository<Domain.Entities.Currency> currencyRepository;

		public CreateCurrencyIdentifiedCommandHandler(
			IMediator mediator,
			IRequestManager requestManager,
			IMapper mapper,
			IRepository<Domain.Entities.Currency> currencyRepository
			) : base(mediator, requestManager)
		{
			this.mapper = mapper;
			this.currencyRepository = currencyRepository;
		}

		public override async Task<ResultWrapper> CreateResultForRequest(IdentifiedCommand<CreateCurrencyCommand, ResultWrapper> request, 
			CancellationToken cancellationToken)
		{
			var currency = mapper.Map<Domain.Entities.Currency>(request.Command.CreateCurrencyIntegrationEvent);

			if (currency.Invalid)
				return ResultWrapper.Error(currency.Notifications);
			
			await currencyRepository.Add(currency);

			return ResultWrapper.Created(currency);
		}

		public override ResultWrapper CreateResultForDuplicateRequest()
		{
			return ResultWrapper.Conflict();
		}
	}
}
