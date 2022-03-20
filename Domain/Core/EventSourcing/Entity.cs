using Domain.Core.Entities;
using System;
using System.Text.Json.Serialization;

namespace Domain.Core.EventSourcing
{
    public abstract class Entity : IEntity, IEventSourcing
    {
        public virtual Guid Id { get; set; }

        public virtual DateTimeOffset CreatedAt { get; set; }

        public virtual DateTimeOffset ChangedAt { get; set; }

        [JsonIgnore]
        public EventCollection Events { get; } = new EventCollection();

        protected Entity(Identity id)
        {
            Id = id;
        }

        protected Entity()
        {
        }
    }
}
