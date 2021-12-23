using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Profiles.Domain.Profiles.Models.ValueObjects.Identifiers;

public class UserId : Identifier<int>
{
    public UserId(int value) : base(value)
    {
    }
}
