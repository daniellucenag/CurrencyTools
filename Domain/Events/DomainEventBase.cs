using System;

namespace Domain.Events
{
    public abstract class DomainEventBase<TModel> : IDomainEvent<TModel> where TModel : struct
    {
        protected DomainEventBase(TModel data) => Data = data;

        public Guid Id => Guid.NewGuid();
        public DateTimeOffset CreatedAt => DateTime.Now;
        public TModel Data { get; }
    }
}
