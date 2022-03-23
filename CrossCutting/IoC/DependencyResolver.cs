using MediatR;
using Application;
using Application.Currency;
using Application.Interfaces;
using Domain.Core.SeedWork;
using Domain.Entities.Currency;
using Infrastructure.Behavior;
using Infrastructure.Idempotency;
using Infrastructure.Publisher;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using Application.Behaviors;

namespace CrossCutting.IoC
{
    [ExcludeFromCodeCoverage]
    public static class DependencyResolver
    {
        public static void AddDependencyResolver(this IServiceCollection services)
        {
            RegisterApplications(services);
            RegisterRepositories(services);
            RegisterTyes(services);          
        }

        private static void RegisterApplications(IServiceCollection services)
        {
            services.AddScoped<ICurrencyApplication, CurrencyApplication>();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IRepository<Currency>, CurrencyRepository>();
        }

        private static void RegisterTyes(IServiceCollection services)
        {
            services.AddScoped<ICreateCurrencyIntegrationEvent, CreateCurrencyIntegrationEvent>();
        }

        public static void AddMediatR(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateCurrencyIdentifiedCommandHandler).Assembly);

            services.AddTransient(typeof(IPipelineBehavior<IdentifiedCommand<CreateCurrencyCommand, ResultWrapper>, ResultWrapper>),
                       typeof(TransactionBehaviour<IdentifiedCommand<CreateCurrencyCommand, ResultWrapper>, ResultWrapper>));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TracingBehavior<,>));

            services.AddScoped<IRequestManager, RequestManager>();
        }

        public static void AddSqlServerConnection(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IDbConnection>(prov => new SqlConnection(connectionString));
        }

        public static IServiceCollection AddPublishers(this IServiceCollection services)
        {
            return services.AddScoped(typeof(IPublisherApplication<>), typeof(PublisherRabbitMq<>));
        }
            
        public static OptionsConfiguration AddOptionsConfiguration(this IServiceCollection services)
        {
            return new OptionsConfiguration(services);
        }
    }
}