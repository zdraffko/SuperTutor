using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.HealthChecks.Models;
using System.Text.Json;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Api.HealthChecks.Extensions;

public static class HealthCheckOptionsExtensions
{
    public static HealthCheckOptions AddCustomResponseWriter(this HealthCheckOptions healthCheckOptions)
    {
        healthCheckOptions.ResponseWriter = async (httpContext, healthReport) =>
        {
            httpContext.Response.ContentType = "application/json";

            var systemHealthCheck = new SystemHealthCheck
            {
                Status = healthReport.Status.ToString(),
                ComponentHealthChecks = healthReport.Entries.Select(healthReportEntry => new ComponentHealthCheck
                {
                    Component = healthReportEntry.Key,
                    Status = healthReportEntry.Value.Status.ToString(),
                    Description = healthReportEntry.Value.Description ?? healthReportEntry.Value.Exception?.Message,
                    Duration = healthReportEntry.Value.Duration
                }),
                TotalDuration = healthReport.TotalDuration
            };

            var systemHealthCheckResponse = JsonSerializer.Serialize(systemHealthCheck);
            await httpContext.Response.WriteAsync(systemHealthCheckResponse);
        };

        return healthCheckOptions;
    }
}
