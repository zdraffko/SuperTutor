using SuperTutor.Contexts.Schedule.Domain.Common.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Schedule.Application.TimeSlots.Commands.AddAvailability;

public class AddTimeSlotAvailabilityCommand : Command
{
    public AddTimeSlotAvailabilityCommand(TutorId tutorId, DateOnly date, TimeOnly startTime)
    {
        TutorId = tutorId;
        Date = date;
        StartTime = startTime;
    }

    public TutorId TutorId { get; }

    public DateOnly Date { get; }

    public TimeOnly StartTime { get; }
}
