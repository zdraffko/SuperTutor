using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Schedule.Domain.TimeSlots.Events;

public class TimeSlotAvailabilityRemovedDomainEvent : DomainEvent
{
    public TimeSlotAvailabilityRemovedDomainEvent(TimeSlotId timeSlotId) => TimeSlotId = timeSlotId;

    public TimeSlotId TimeSlotId { get; }
}
