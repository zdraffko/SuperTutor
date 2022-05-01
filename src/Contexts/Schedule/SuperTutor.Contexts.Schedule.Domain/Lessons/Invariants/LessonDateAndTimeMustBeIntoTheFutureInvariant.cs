using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Schedule.Domain.Lessons.Invariants;

public class LessonDateAndTimeMustBeIntoTheFutureInvariant : Invariant
{
    private readonly DateOnly date;
    private readonly TimeOnly time;

    public LessonDateAndTimeMustBeIntoTheFutureInvariant(DateOnly date, TimeOnly time)
        : base("The date and time for the lesson must be into the future")
    {
        this.date = date;
        this.time = time;
    }

    public override bool IsValid() => date.ToDateTime(time) >= DateTime.UtcNow;
}
