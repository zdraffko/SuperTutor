namespace SuperTutor.SharedLibraries.BuildingBlocks.Api.HealthChecks.Models;

public readonly record struct ComponentHealthCheck(string Component, string Status, string? Description, TimeSpan Duration);
