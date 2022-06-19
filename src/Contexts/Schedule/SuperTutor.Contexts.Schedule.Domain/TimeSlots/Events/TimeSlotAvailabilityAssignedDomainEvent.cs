using SuperTutor.Contexts.Schedule.Domain.TimeSlots.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Schedule.Domain.TimeSlots.Events;

public class TimeSlotAvailabilityAssignedDomainEvent : DomainEvent
{
    public TimeSlotAvailabilityAssignedDomainEvent(TimeSlotId timeSlotId, TimeSlotStatus status)
    {
        TimeSlotId = timeSlotId;
        Status = status;
    }

    public TimeSlotId TimeSlotId { get; }

    public TimeSlotStatus Status { get; }
}
