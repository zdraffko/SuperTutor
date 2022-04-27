using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Schedule.Domain.Lessons.Models.Enumerations;

public sealed class LessonType : Enumeration
{
    private LessonType(int value, string name) : base(value, name) { }

    public static readonly LessonType Trial = new(1, nameof(Trial));

    public static readonly LessonType Regular = new(2, nameof(Regular));
}
