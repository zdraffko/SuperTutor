namespace SuperTutor.SharedLibraries.BuildingBlocks.Api.HealthChecks.Models;

public readonly record struct SystemHealthCheck(string Status, IEnumerable<ComponentHealthCheck> ComponentHealthChecks, TimeSpan TotalDuration);
