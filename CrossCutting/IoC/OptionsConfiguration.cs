using Infrastructure.Idempotency;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CrossCutting.IoC
{
    [ExcludeFromCodeCoverage]
    public class OptionsConfiguration
    {
        private readonly IServiceCollection services;
        public OptionsConfiguration(IServiceCollection services)
        {
            this.services = services;
        }

        public OptionsConfiguration ConfigureMyOptions(IConfiguration configuration)
        {
            services.Configure<IdempotencyCacheOptions>(configuration.GetSection("Caching"));

            return this;
        }
    }
}
