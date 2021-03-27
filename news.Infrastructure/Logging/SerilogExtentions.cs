using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Formatting.Elasticsearch;
using Serilog.Events;
using Serilog.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog.Sinks.Kafka;

namespace news.Infrastructure.Logging
{
    public static class SerilogExtentions
    {
        public static void CreateLoggerConfiguration(this IServiceProvider serviceProvider, IWebHostEnvironment env)
        {
            var config = serviceProvider.GetService<IConfiguration>();

            var loggerConfig = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails();

            if (env.IsDevelopment())
            {
                loggerConfig.WriteTo.Async(a => a.Console());
            }

            if (config.GetValue<int>("AppSettings:Logging:EnableTxt", 0).Equals(1))
            {
                loggerConfig.WriteTo.Map("LogFolder", "Other",
                (name, wt) =>
                    wt.Async(a =>
                    a.File(
                        new ElasticsearchJsonFormatter(),
                        $"./ErrorLogs/{name}/log-.txt",
                        rollingInterval: RollingInterval.Day)));
            }

            if (config.GetValue<int>("AppSettings:Logging:EnableEs", 0).Equals(1))
            {
                loggerConfig.WriteTo.Async(a =>
                a.Kafka(
                    bootstrapServers: config.GetValue<string>("AppSettings:Logging:EsUrl", string.Empty),
                    topic: config.GetValue<string>("AppSettings:Logging:EsTopic", string.Empty),
                    formatter: new ElasticsearchJsonFormatter()));
            }
            Log.Logger = loggerConfig.CreateLogger();
        }
    }
}
