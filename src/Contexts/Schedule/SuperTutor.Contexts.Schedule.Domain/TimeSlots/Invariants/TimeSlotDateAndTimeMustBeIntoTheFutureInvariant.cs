using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Schedule.Domain.TimeSlots.Invariants;

public class TimeSlotDateAndTimeMustBeIntoTheFutureInvariant : Invariant
{
    private readonly DateOnly date;
    private readonly TimeOnly time;

    public TimeSlotDateAndTimeMustBeIntoTheFutureInvariant(DateOnly date, TimeOnly time)
        : base($"The date and time for the time slot must be at least '{TimeSlot.Duration.TotalMinutes}' minutes into the future")
    {
        this.date = date;
        this.time = time;
    }

    public override bool IsValid() => date.ToDateTime(time) >= DateTime.UtcNow.AddMinutes(TimeSlot.Duration.TotalMinutes);
}
