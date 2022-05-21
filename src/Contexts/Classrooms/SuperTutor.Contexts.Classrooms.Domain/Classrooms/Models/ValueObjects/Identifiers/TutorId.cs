using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Classrooms.Domain.Classrooms.Models.ValueObjects.Identifiers;

public class TutorId : Identifier<Guid>
{
    public TutorId(Guid value) : base(value)
    {
    }
}
