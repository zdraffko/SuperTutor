using SuperTutor.Contexts.Schedule.Domain.TimeSlots;
using SuperTutor.Contexts.Schedule.Domain.TimeSlots.Models.Enumerations;
using SuperTutor.Contexts.Schedule.Domain.TimeSlots.Models.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Schedule.Infrastructure.Persistence.TimeSlots.QueryModels;

public class TimeSlotQueryModel
{
    public TimeSlotId Id { get; init; }

    public TutorId TutorId { get; init; }

    public DateOnly Date { get; init; }

    public TimeOnly StartTime { get; init; }

    public TimeSlotType Type { get; init; }

    public TimeSlotStatus Status { get; init; }
}
