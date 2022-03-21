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

        public static string GetRabbitMqConnectionString(this IConfiguration config) => config.GetValue<string>("RABBIT_MQ_CONNECTION_STRING");
    }
}
