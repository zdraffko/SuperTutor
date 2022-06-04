using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Schedule.Domain.Lessons.Models.Enumerations;

public sealed class LessonStatus : Enumeration
{
    public LessonStatus(int value, string name) : base(value, name) { }

    public static readonly LessonStatus Reserved = new(1, "Резервиран");

    public static readonly LessonStatus Scheduled = new(2, "Насрочен");

    public static readonly LessonStatus Started = new(3, "Започнал");

    public static readonly LessonStatus Ended = new(4, "Приключил");

    public static readonly LessonStatus Completed = new(5, "Завършен");

    public static readonly LessonStatus Rescheduled = new(6, "Пренасрочен");

    public static readonly LessonStatus Canceled = new(7, "Отказан");

    public static readonly LessonStatus Abandoned = new(8, "Изоставен");
}
