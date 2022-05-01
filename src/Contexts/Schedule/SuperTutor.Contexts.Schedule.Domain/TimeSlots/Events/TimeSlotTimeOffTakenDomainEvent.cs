using SuperTutor.Contexts.Schedule.Domain.TimeSlots.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Schedule.Domain.TimeSlots.Events;

public class TimeSlotTimeOffTakenDomainEvent : DomainEvent
{
    public TimeSlotTimeOffTakenDomainEvent(
        TimeSlotId timeSlotId,
        TutorId tutorId,
        DateOnly date,
        TimeOnly startTime,
        int type,
        int status)
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

    public int Type { get; }

    public int Status { get; }
}
