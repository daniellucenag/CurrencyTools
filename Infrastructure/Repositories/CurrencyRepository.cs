using Dapper;
using Domain.Core.SeedWork;
using Domain.Entities.CurrencyContext;
using Infrastructure.Publisher;
using Infrastructure.Utils;
using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    [ExcludeFromCodeCoverage]
    public class CurrencyRepository : IRepository<Currency>
    {
        private readonly IDbConnection Connection;

        private readonly IDomainEventHandler DomainEvent;

        public CurrencyRepository(IDbConnection connection, IDomainEventHandler domainEvent)
        {
            Connection = connection ?? throw new ArgumentNullException(nameof(connection));
            DomainEvent = domainEvent;
        }

        public async Task Add(Currency item)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CurrencyId", item.Id, DbType.Guid);
            parameters.Add("@Name", item.Name, DbType.AnsiString);
            parameters.Add("@Description", item.Description, DbType.AnsiString);
            parameters.Add("@CurrencyApiCode", item.CurrencyApiCode, DbType.AnsiString);
            parameters.Add("@CreatedAt", item.CreatedAt, DbType.DateTimeOffset);
            await Connection.ExecuteAsync(SqlQueries.SQL_INSERT_CURRENCY, parameters);

            DomainEvent.AddDomainEvent(item.DomainEvents.ToArray());
            if (DomainEvent != null)
                await DomainEvent.Handler();
        }
    }
}
