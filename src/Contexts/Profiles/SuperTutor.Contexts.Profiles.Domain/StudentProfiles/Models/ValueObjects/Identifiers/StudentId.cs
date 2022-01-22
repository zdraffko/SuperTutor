using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Profiles.Domain.StudentProfiles.Models.ValueObjects.Identifiers;

public class StudentId : GuidIdentifier
{
    public StudentId(Guid value) : base(value)
    {
    }
}
