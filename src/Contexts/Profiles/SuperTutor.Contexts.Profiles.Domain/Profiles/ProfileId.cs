using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Profiles.Domain.Profiles;

public class ProfileId : Identifier<int>
{
    public ProfileId(int value) : base(value)
    {
    }
}
