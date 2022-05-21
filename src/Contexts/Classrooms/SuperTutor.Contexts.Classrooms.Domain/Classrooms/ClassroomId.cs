using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Classrooms.Domain.Classrooms;

public class ClassroomId : Identifier<Guid>
{
    public ClassroomId(Guid value) : base(value)
    {
    }
}
