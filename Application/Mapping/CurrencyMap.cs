using Application.Currency;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mapping
{
    public class CurrencyMap : Profile
    {
        public CurrencyMap()
        {
            CreateMap<ICreateCurrencyIntegrationEvent, Domain.Entities.Currency>()
                .ConstructUsing(src => new Domain.Entities.Currency(
                    src.Name,
                    src.Description,
                    src.Id));
        }
    }
}
