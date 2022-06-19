using SuperTutor.Contexts.Schedule.Domain.Common.Models.ValueObjects.Identifiers;
using SuperTutor.Contexts.Schedule.Domain.TimeSlots.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Schedule.Domain.TimeSlots.Events;

public class TimeSlotAvailabilityAddedDomainEvent : DomainEvent
{
    public TimeSlotAvailabilityAddedDomainEvent(
        TimeSlotId timeSlotId,
        TutorId tutorId,
        DateOnly date,
        TimeOnly startTime,
        TimeSlotType type,
        TimeSlotStatus status)
    {
        TimeSlotId = timeSlotId;
        TutorId = tutorId;
        Date = date;
        StartTime = startTime;
        Type = type;
        Status = status;
    }

    public TimeSlotId TimeSlotId { get; }

    public TutorId TutorId { get; }

    public DateOnly Date { get; }

    public TimeOnly StartTime { get; }

    public TimeSlotType Type { get; }

    public TimeSlotStatus Status { get; }
}
