using Application.CurrencyContext;
using AutoMapper;

namespace Application.Mapping
{
    public class CurrencyMap : Profile
    {
        public CurrencyMap()
        {
            CreateMap<ICreateCurrencyIntegrationEvent, Domain.Entities.CurrencyContext.Currency>()
                .ConstructUsing(src => new Domain.Entities.CurrencyContext.Currency(
                    src.Name,
                    src.Description,
                    src.CurrencyApiCode,
                    src.Id));
        }
    }
}
