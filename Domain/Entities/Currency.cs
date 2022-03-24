using Domain.Core.Entities;
using Flunt.Validations;
using System;

namespace Domain.Entities
{
    public class Currency : RootEntity, IAggregateRoot
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }

        public Currency(string name, string description, Guid? identifid = null)
        {
            Name = name;
            Description = description;
            Id = identifid ?? Id;

            AddNotifications(new Contract()
              .Requires()
              .IsNotNullOrEmpty(Name, nameof(Name), $"{nameof(Name)} can't be null or empty")
              .IsNotNullOrEmpty(Description, nameof(Description), $"{nameof(Description)} can't be null or empty")
            );
        }

        protected Currency()
        { }

    }
}
