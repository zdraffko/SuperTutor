using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Schedule.Domain.Lessons.Models.Enumerations;

public sealed class LessonStatus : Enumeration
{
    public LessonStatus(int value, string name) : base(value, name) { }

    public static readonly LessonStatus Reserved = new(1, nameof(Reserved));

    public static readonly LessonStatus Scheduled = new(2, nameof(Scheduled));

    public static readonly LessonStatus Started = new(3, nameof(Started));

    public static readonly LessonStatus Ended = new(4, nameof(Ended));

    public static readonly LessonStatus Completed = new(5, nameof(Completed));

    public static readonly LessonStatus Rescheduled = new(6, nameof(Rescheduled));

    public static readonly LessonStatus Canceled = new(7, nameof(Canceled));

    public static readonly LessonStatus Abandoned = new(8, nameof(Abandoned));
}
