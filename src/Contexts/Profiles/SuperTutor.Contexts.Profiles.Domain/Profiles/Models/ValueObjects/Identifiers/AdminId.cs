using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Profiles.Domain.Profiles.Models.ValueObjects.Identifiers;

public class AdminId : GuidIdentifier
{
    public AdminId(Guid value) : base(value)
    {
    }
}
