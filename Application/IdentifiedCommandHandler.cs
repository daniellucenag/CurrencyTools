using Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application
{
    public class IdentifiedCommandHandler<T, R> : IRequestHandler<IdentifiedCommand<T, R>, R> where T : IRequest<R>
    {
        protected readonly IMediator mediator;
        private readonly IRequestManager requestManager;

        public IdentifiedCommandHandler(
            IMediator mediator,
            IRequestManager requestManager)
        {
            this.mediator = mediator;
            this.requestManager = requestManager;
        }

        public virtual R CreateResultForDuplicateRequest()
        {
            return default(R);
        }

        public async Task<R> Handle(IdentifiedCommand<T, R> request, CancellationToken cancellationToken)
        {
            var alreadyExists = await this.requestManager.ExistAsync<T>(request.Id);
            if (alreadyExists)
            {
                return CreateResultForDuplicateRequest();
            }
            else
            {
                await requestManager.CreateRequestForCommandAsync<T>(request.Id);
                return await CreateResultForRequest(request, cancellationToken);
            }
        }

        public virtual async Task<R> CreateResultForRequest(IdentifiedCommand<T, R> request, CancellationToken cancellationToken)
        {
            return await mediator.Send(request.Command, cancellationToken);
        }
    }
}
