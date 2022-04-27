using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Schedule.Domain.Lessons;

public class LessonId : Identifier<Guid>
{
    public LessonId(Guid value) : base(value)
    {
    }
}
