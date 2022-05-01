using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Schedule.Domain.Lessons.Models.ValueObjects.Identifiers;

public class StudentId : Identifier<Guid>
{
    public StudentId(Guid value) : base(value)
    {
    }
}
