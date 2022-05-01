using SuperTutor.Contexts.Schedule.Domain.TimeSlots;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Schedule.Application.TimeSlots.Commands.RemoveAvailability;

public class RemoveTimeSlotAvailabilityCommand : Command
{
    public RemoveTimeSlotAvailabilityCommand(TimeSlotId timeSlotId) => TimeSlotId = timeSlotId;

    public TimeSlotId TimeSlotId { get; }
}
