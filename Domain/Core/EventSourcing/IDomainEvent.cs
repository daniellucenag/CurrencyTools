using System;

namespace Domain.Core.EventSourcing
{
    public interface IDomainEvent
    {
        Guid Id
        {
            get;
        }

        Guid Track
        {
            get;
        }

        DateTimeOffset CreatedAt
        {
            get;
        }

        internal object GetData();
    }
}
