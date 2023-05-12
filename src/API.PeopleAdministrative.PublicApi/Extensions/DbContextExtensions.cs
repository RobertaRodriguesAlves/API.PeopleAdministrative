using API.PeopleAdministrative.Infrastructure.Data.Context;
using API.PeopleAdministrative.Shared.AppSettings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace API.PeopleAdministrative.PublicApi.Extensions;

public static class DbContextExtensions
{
    private static readonly string AssemblyName = typeof(Program).Assembly.GetName().Name;

    public static void AddPeopleAdministrativeDbContext(this IServiceCollection services)
    {
        services.AddDbContext<PeopleAdministrativeContext>((serviceProvider, optionsBuilder) =>
        {
            var connectionString
                = serviceProvider.GetRequiredService<IOptions<ConnectionStrings>>().Value;

            optionsBuilder.UseSqlServer(connectionString.Database, sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(AssemblyName);

                // Configura a resiliência da conexão: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency
                sqlOptions.EnableRetryOnFailure(maxRetryCount: 3);

                // Em ambiente de desenvolvimento é logado informações detalhadas.
                var environment = serviceProvider.GetRequiredService<IHostEnvironment>();
                if (environment.IsDevelopment())
                    optionsBuilder.EnableDetailedErrors().EnableSensitiveDataLogging();
            });

            var logger = serviceProvider.GetRequiredService<ILogger<PeopleAdministrativeContext>>();

            // Log tentativas de repetição
            optionsBuilder.LogTo(
                filter: (eventId, _) => eventId.Id == CoreEventId.ExecutionStrategyRetrying,
                logger: (eventData) =>
                {
                    var retryEventData = eventData as ExecutionStrategyEventData;
                    var exceptions = retryEventData.ExceptionsEncountered;
                    var count = exceptions.Count;
                    var delay = retryEventData.Delay;
                    var message = exceptions[exceptions.Count - 1]?.Message;
                    logger.LogWarning("----- Retry #{Count} with delay {Delay} due to error: {Message}.", count, delay, message);
                });
        });
    }
}