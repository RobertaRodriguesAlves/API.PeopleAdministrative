using API.PeopleAdministrative.Shared.Extensions;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.PeopleAdministrative.PublicApi.Extensions;

public static class HealthCheckExtensions
{
    public static IHealthChecksBuilder AddHealthChecks(this IServiceCollection services, string connectionString)
    {
        Guard.Against.NullOrWhiteSpace(connectionString);
        return services.AddHealthChecks().AddCheck<GCInfoHealthCheck>("GCInfoCheck", HealthStatus.Degraded, new string[1] { "memory" }).AddMySql(connectionString, "SELECT @@VERSION;", HealthStatus.Degraded, new string[3] { "db", "sql", "mysql" });
    }

    public static void UseHealthChecks(this WebApplication app)
    {
        app.UseHealthChecks("/health", new HealthCheckOptions
        {
            AllowCachingResponses = false,
            ResponseWriter = WriteResponse
        });
    }

    public static Task WriteResponse(HttpContext context, HealthReport report)
    {
        context.Response.ContentType= "application/json";
        var value = new
        {
            Status = report.Status.ToString(),
            Duration = report.TotalDuration,
            Info = Enumerable.Select(report.Entries, (KeyValuePair<string, HealthReportEntry> e) => new
            {
                e.Key,
                e.Value.Description,
                e.Value.Duration,
                Status = Enum.GetName(typeof(HealthStatus), e.Value.Status),
                Error = e.Value.Exception?.Message,
                e.Value.Data
            }).ToList()
        };
        return context.Response.WriteAsync(value.ToJson());
    }
}

public sealed class GCInfoHealthCheck : IHealthCheck
{
    private const long Threshold = 1073741824L;
    private const string Description = "O status é degradado se a quantidade de bytes alocados for >= 1gb";
    private static readonly string[] SizeSuffixes = new string[9] { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
    {
        long totalMemory = GC.GetTotalMemory(forceFullCollection: false);
        GCMemoryInfo gCMemoryInfo = GC.GetGCMemoryInfo();
        Dictionary<string, object> data = new Dictionary<string, object>
        {
            {
                "Allocated",
                SizeSuffix(totalMemory)
            },
            {
                "TotalAvailableMemoryBytes",
                SizeSuffix(gCMemoryInfo.TotalAvailableMemoryBytes)
            },
            {
                "Gen0Collections",
                GC.CollectionCount(0)
            },
            {
                "Gen1Collections",
                GC.CollectionCount(1)
            },
            {
                "Gen2Collections",
                GC.CollectionCount(2)
            }
        };
        HealthStatus status = ((totalMemory >= Threshold) ? context.Registration.FailureStatus : HealthStatus.Healthy);
        return Task.FromResult(new HealthCheckResult(status, Description, null, data));
    }

    private static string SizeSuffix(long allocatedMemory, int decimalPlaces = 1)
    {
        if (allocatedMemory < 0)
        {
            return "-" + SizeSuffix(allocatedMemory, decimalPlaces);
        }

        if (allocatedMemory == 0)
        {
            return string.Format("{0:n" + decimalPlaces + "} bytes", 0);
        }

        int num = (int)Math.Log(allocatedMemory, 1024.0);
        decimal num2 = (decimal)allocatedMemory / (1L << num * 10);
        if (Math.Round(num2, decimalPlaces) >= 1000m)
        {
            num++;
            num2 /= 1024m;
        }

        return string.Format("{0:n" + decimalPlaces + "} {1}", num2, SizeSuffixes[num]);
    }
}
