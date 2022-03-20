using MediatR;
using System.Collections.Generic;

namespace Domain.Core.Entities
{
    public abstract class RootEntity : BaseEntity, IDomainEntityEvent
    {
        #region DomainEvents
        private readonly List<INotification> domainEvents = new List<INotification>();
        public IReadOnlyCollection<INotification> DomainEvents => domainEvents.AsReadOnly();
        public void AddDomainEvent(INotification eventItem)
        {
            if (Valid)
                domainEvents.Add(eventItem);
        }
        #endregion
    }
}
