using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Schedule.Domain.Lessons.Models.Enumerations;

public sealed class LessonStatus : Enumeration
{
    private LessonStatus(int value, string name) : base(value, name) { }

    public static readonly LessonStatus Reserved = new(1, nameof(Reserved));

    public static readonly LessonStatus Booked = new(2, nameof(Booked));

    public static readonly LessonStatus Completed = new(3, nameof(Completed));

    public static readonly LessonStatus Rescheduled = new(4, nameof(Rescheduled));

    public static readonly LessonStatus Canceled = new(5, nameof(Canceled));

    public static readonly LessonStatus Abandoned = new(6, nameof(Abandoned));
}
