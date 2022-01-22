using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Profiles.Domain.TutorProfiles;

public class TutorProfileId : GuidIdentifier
{
    public TutorProfileId(Guid value) : base(value)
    {
    }
}
