using Application;
using Application.Interfaces;
using Domain.Core.SeedWork;
using Domain.Entities.Currency;
using Infrastructure.Behavior;
using Infrastructure.Publisher;
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
            RegisterApplications(services);
            RegisterRepositories(services);
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IRepository<Currency>, CurrencyRepository>();
        }

        private static void RegisterApplications(IServiceCollection services)
        {
            services.AddScoped<ICurrencyApplication, CurrencyApplication>();
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

        public static IServiceCollection AddPublishers(this IServiceCollection services)
        {
            return services.AddScoped(typeof(IPublisherApplication<>), typeof(PublisherRabbitMq<>));
        }
    }
}