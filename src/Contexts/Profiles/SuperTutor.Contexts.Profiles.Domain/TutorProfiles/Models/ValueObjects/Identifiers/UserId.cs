using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.ValueObjects.Identifiers;

public class UserId : GuidIdentifier
{
    public UserId(Guid value) : base(value)
    {
    }
}
