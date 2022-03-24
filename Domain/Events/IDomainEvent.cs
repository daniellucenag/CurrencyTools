using MediatR;
using System;

namespace Domain.Events
{
    public interface IDomainEvent<out TModel> : INotification where TModel : struct
    {
        public Guid Id { get; }
        public DateTimeOffset CreatedAt { get; }
        TModel Data { get; }
    }
}
