using SuperTutor.Contexts.Schedule.Domain.Common.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Schedule.Domain.TimeSlots.Events;

public class TimeSlotTimeOffTakenDomainEvent : DomainEvent
{
    public TimeSlotTimeOffTakenDomainEvent(
        TimeSlotId timeSlotId,
        TutorId tutorId,
        DateOnly date,
        TimeOnly startTime)
    {
        TimeSlotId = timeSlotId;
        TutorId = tutorId;
        Date = date;
        StartTime = startTime;
    }

    public TimeSlotId TimeSlotId { get; }

    public TutorId TutorId { get; }

    public DateOnly Date { get; }

    public TimeOnly StartTime { get; }
}
