using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Currency
{
	public class CreateCurrencyIdentifiedCommandHandler : IdentifiedCommandHandler<CreateCurrencyCommand, ResultWrapper>
	{
		private readonly IMapper mapper;

		public CreateCurrencyIdentifiedCommandHandler(
			IMediator mediator,
			IRequestManager requestManager,
			IMapper mapper
			) : base(mediator, requestManager)
		{
			this.mapper = mapper;
		}

		public override async Task<ResultWrapper> CreateResultForRequest(IdentifiedCommand<CreateCurrencyCommand, ResultWrapper> request, 
			CancellationToken cancellationToken)
		{
			//mapear para domain
			//salvar repositorio

			throw new NotImplementedException();
		}

		public override ResultWrapper CreateResultForDuplicateRequest()
		{
			return ResultWrapper.Conflict();
		}
	}
}
