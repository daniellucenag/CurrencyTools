using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;
using static System.String;

namespace CrossCutting.Util
{
    [ExcludeFromCodeCoverage]
    public static class ConfigurationExtension
    {
        public static string GetSqlConnectionString(this IConfiguration config) => Format(config.GetValue<string>("SQL_SERVER_CONNECTION"),
                                                                                          config.GetValue<string>("DB_CURRENCYTOOLS_DATABASE"),
                                                                                          config.GetValue<string>("DB_CURRENCYTOOLS_USER"),
                                                                                          config.GetValue<string>("DB_CURRENCYTOOLS_PASSWORD"));

        public static string GetRabbitMqHost(this IConfiguration config) => config.GetValue<string>("RABBITMQ_HOST");
        public static int GetRabbitMqPort(this IConfiguration config) => config.GetValue<int>("RABBITMQ_PORT");
        public static string GetRabbitMqVirtualHost(this IConfiguration config) => config.GetValue<string>("RABBITMQ_VHOST");
        public static string GetRabbitMqUser(this IConfiguration config) => config.GetValue<string>("RABBITMQ_USER");
        public static string GetRabbitMqPassword(this IConfiguration config) => config.GetValue<string>("RABBITMQ_PASSWORD");

    }
}
