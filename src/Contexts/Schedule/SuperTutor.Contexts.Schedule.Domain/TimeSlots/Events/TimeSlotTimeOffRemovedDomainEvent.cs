using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Schedule.Domain.TimeSlots.Events;

public class TimeSlotTimeOffRemovedDomainEvent : DomainEvent
{
    public TimeSlotTimeOffRemovedDomainEvent(TimeSlotId timeSlotId) => TimeSlotId = timeSlotId;

    public TimeSlotId TimeSlotId { get; }
}
