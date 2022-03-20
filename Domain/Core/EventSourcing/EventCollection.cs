using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Domain.Core.EventSourcing
{
    public class EventCollection : ReadOnlyCollection<IDomainEvent>
    {
        internal EventCollection()
            : base((IList<IDomainEvent>)new List<IDomainEvent>())
        {
        }

        public void Add(IDomainEvent @event)
        {
            base.Items.Add(@event);
        }

        internal void Flush()
        {
            base.Items.Clear();
        }
    }
}
