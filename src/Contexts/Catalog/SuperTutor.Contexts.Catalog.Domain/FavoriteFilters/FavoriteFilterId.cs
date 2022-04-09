using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Catalog.Domain.FavoriteFilters;

public class FavoriteFilterId : Identifier<Guid>
{
    public FavoriteFilterId(Guid value) : base(value)
    {
    }
}
