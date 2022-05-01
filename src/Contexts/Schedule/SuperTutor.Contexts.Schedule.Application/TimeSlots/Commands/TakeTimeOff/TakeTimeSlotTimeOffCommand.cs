using SuperTutor.Contexts.Schedule.Domain.TimeSlots.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Schedule.Application.TimeSlots.Commands.TakeTimeOff;

public class TakeTimeSlotTimeOffCommand : Command
{
    public TakeTimeSlotTimeOffCommand(TutorId tutorId, DateOnly date, TimeOnly startTime)
    {
        TutorId = tutorId;
        Date = date;
        StartTime = startTime;
    }

    public TutorId TutorId { get; }

    public DateOnly Date { get; }

    public TimeOnly StartTime { get; }
}
