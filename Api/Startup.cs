using Api.Filters;
using Api.Middlewares;
using Application.CurrencyContext;
using CrossCutting.Assemblies;
using CrossCutting.IoC;
using CrossCutting.Util;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMassTransit(ms =>
            {
                ms.AddDelayedMessageScheduler();

                ms.AddConsumer<CreateCurrencyConsumer>();

                ms.SetKebabCaseEndpointNameFormatter();

                ms.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(Configuration.GetRabbitMqHost(), cfgr =>
                    {
                        cfgr.Username(Configuration.GetRabbitMqUser());
                        cfgr.Password(Configuration.GetRabbitMqPassword());
                    });

                    cfg.UseDelayedMessageScheduler();
                    cfg.ConfigureEndpoints(ctx, new KebabCaseEndpointNameFormatter(false));
                    cfg.UseMessageRetry(retry => { retry.Interval(3, TimeSpan.FromSeconds(5)); });
                });
            });

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            });
            services.AddHttpContextAccessor();
            services.AddMvc(options => options.Filters.Add(new DefaultExceptionFilterAttribute()));

            services.AddAutoMapper(AssemblyUtil.GetCurrentAssemblies());

            services.AddSqlServerConnection(Configuration.GetSqlConnectionString());

            services.AddDependencyResolver();

            services.AddHttpClient();

            services.AddMediatR();

            services.AddHealthChecks();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "CurrencyTolls",
                    Description = "API - CurrencyTolls",
                    Version = "v1"
                });

                var apiPath = Path.Combine(AppContext.BaseDirectory, "Api.xml");
                var applicationPath = Path.Combine(AppContext.BaseDirectory, "Application.xml");

                c.UseAllOfForInheritance();

                c.IncludeXmlComments(apiPath);
                c.IncludeXmlComments(applicationPath);
            });

            services.AddOptionsConfiguration().ConfigureMyOptions(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRequestResponseLogging();

            app.UsePathBase("/CurrencyTolls");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/CurrencyTolls/swagger/v1/swagger.json", "API CurrencyTolls");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}