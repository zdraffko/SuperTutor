using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.ValueObjects.Identifiers;

public class AdminId : Identifier<Guid>
{
    public AdminId(Guid value) : base(value)
    {
    }
}
