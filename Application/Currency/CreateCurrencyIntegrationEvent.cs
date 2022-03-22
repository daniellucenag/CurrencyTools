﻿using Domain.Core;
using System;

namespace Application
{
    public class CreateCurrencyIntegrationEvent : IntegrationEvent<CreateCurrency>
    {
        public CreateCurrencyIntegrationEvent(CreateCurrency createCurrency) : base(createCurrency)
        {            
        }
    }

    public struct CreateCurrency
    {
        public CreateCurrency(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}