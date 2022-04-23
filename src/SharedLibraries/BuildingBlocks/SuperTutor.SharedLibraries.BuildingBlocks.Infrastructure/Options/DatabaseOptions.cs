namespace SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure.Options;

public class DatabaseOptions
{
    public const string OptionsSectionName = "Database";

    public string ConnectionString { get; set; } = default!;
}
