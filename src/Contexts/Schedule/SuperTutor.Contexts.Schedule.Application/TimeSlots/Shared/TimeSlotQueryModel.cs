using SuperTutor.Contexts.Schedule.Domain.Common.Models.ValueObjects.Identifiers;
using SuperTutor.Contexts.Schedule.Domain.TimeSlots;

namespace SuperTutor.Contexts.Schedule.Application.TimeSlots.Shared;

public class TimeSlotQueryModel
{
    public TimeSlotId Id { get; init; }

    public TutorId TutorId { get; init; }

    public DateTime Date { get; init; }

    public TimeSpan StartTime { get; init; }

    public string Type { get; init; }

    public string Status { get; init; }
}
