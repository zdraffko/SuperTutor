namespace SuperTutor.Contexts.Catalog.Tests.Application.Behavior.Shared;

internal static class Constants
{
    public const string DatabaseConnectionString = "Server=localhost;Database=SuperTutorCatalogTest;User Id=sa;Password=testPass123;MultipleActiveResultSets=true";

    public const string AddFavoriteFilterEndpoint = "/api/Students/AddFavoriteFilter";

    public const string RemoveFavoriteFilterEndpoint = "/api/Students/RemoveFavoriteFilter";
}
