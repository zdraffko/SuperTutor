using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Profiles.Domain.StudentProfiles;

public class StudentProfileId : GuidIdentifier
{
    public StudentProfileId(Guid value) : base(value)
    {
    }
}
