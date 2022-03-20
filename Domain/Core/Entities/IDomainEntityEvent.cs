using MediatR;
using System.Collections.Generic;

namespace Domain.Core.Entities
{
    public interface IDomainEntityEvent
    {
        IReadOnlyCollection<INotification> DomainEvents { get; }
    }
}
