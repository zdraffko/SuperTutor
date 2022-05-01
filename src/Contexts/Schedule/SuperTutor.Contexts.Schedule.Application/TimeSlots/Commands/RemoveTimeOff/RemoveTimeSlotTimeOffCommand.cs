using SuperTutor.Contexts.Schedule.Domain.TimeSlots;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Schedule.Application.TimeSlots.Commands.RemoveTimeOff;

public class RemoveTimeSlotTimeOffCommand : Command
{
    public RemoveTimeSlotTimeOffCommand(TimeSlotId timeSlotId) => TimeSlotId = timeSlotId;

    public TimeSlotId TimeSlotId { get; }
}
