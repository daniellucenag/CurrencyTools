using Domain.Core.SeedWork;
using Domain.Entities.Currency;
using Infrastructure.Behavior;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;

namespace CrossCutting.IoC
{
    public static class DependencyResolver
    {
        public static void AddDependencyResolver(this IServiceCollection services)
        {
            RegisterRepositories(services);
        }

        private static void RegisterRepositories(IServiceCollection services)
        {           
            services.AddScoped<IRepository<Currency>, CurrencyRepository>();         
        }

        public static void AddMediatR(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TracingBehavior<,>));
        }

        public static void AddSqlServerConnection(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IDbConnection>(prov => new SqlConnection(connectionString));
        }
    }
}
