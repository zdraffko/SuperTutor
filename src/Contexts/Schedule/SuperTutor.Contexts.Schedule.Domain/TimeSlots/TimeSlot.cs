using SuperTutor.Contexts.Schedule.Domain.TimeSlots.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities.Aggregates;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Schedule.Domain.TimeSlots;

public class TimeSlot : AggregateRoot<TimeSlotId, Guid>
{
    public static readonly TimeSpan Duration = TimeSpan.FromMinutes(30);

    public TimeSlot(TimeSlotType type) : base(new TimeSlotId(Guid.NewGuid()))
    {
        Type = type;
        Status = type == TimeSlotType.TimeOff ? TimeSlotStatus.Assigned : TimeSlotStatus.Unassigned;
    }

    public DateOnly Date { get; }

    public TimeOnly StartTime { get; }

    public TimeSlotType Type { get; }

    public TimeSlotStatus Status { get; }

    protected override void ApplyDomainEvent(DomainEvent domainEvent) => throw new NotImplementedException();
}
