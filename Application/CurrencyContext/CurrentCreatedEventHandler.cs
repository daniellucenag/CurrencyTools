using Domain.Events;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CurrencyContext
{
    public class CurrentCreatedEventHandler : INotificationHandler<CurrencyCreatedEvent>
    {
        public Task Handle(CurrencyCreatedEvent notification, CancellationToken cancellationToken)
        {
            //postar na fila que atualiza o valor da cotacao da currency
            throw new NotImplementedException();
        }
    }
}