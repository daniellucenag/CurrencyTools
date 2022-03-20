using System;

namespace Domain.Core.Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }

        DateTimeOffset CreatedAt { get; set; }

        DateTimeOffset ChangedAt { get; set; }
    }
}
