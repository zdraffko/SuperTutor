using SuperTutor.Contexts.Schedule.Domain.TimeSlots.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Schedule.Domain.TimeSlots.Events;

public class TimeSlotAvailabilityUnassignedDomainEvent : DomainEvent
{
    public TimeSlotAvailabilityUnassignedDomainEvent(TimeSlotId timeSlotId, TimeSlotStatus status)
    {
        TimeSlotId = timeSlotId;
        Status = status;
    }

    public TimeSlotId TimeSlotId { get; }

    public TimeSlotStatus Status { get; }
}
