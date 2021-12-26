using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Profiles.Domain.Profiles;

public class ProfileId : GuidIdentifier
{
    public ProfileId(Guid value) : base(value)
    {
    }
}
