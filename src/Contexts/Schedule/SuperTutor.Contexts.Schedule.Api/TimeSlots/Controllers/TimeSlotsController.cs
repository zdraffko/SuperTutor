using Microsoft.AspNetCore.Mvc;
using SuperTutor.Contexts.Schedule.Application.TimeSlots.Commands.AddAvailability;
using SuperTutor.Contexts.Schedule.Application.TimeSlots.Commands.TakeTimeOff;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Schedule.Api.TimeSlots.Controllers;

public class TimeSlotsController : ApiController
{
    private readonly ICommandHandler<AddTimeSlotAvailabilityCommand> addTimeSlotAvailabilityCommandHandler;
    private readonly ICommandHandler<TakeTimeSlotTimeOffCommand> takeTimeSlotTimeOffCommandHandler;

    public TimeSlotsController(
        ICommandHandler<AddTimeSlotAvailabilityCommand> addTimeSlotAvailabilityCommandHandler,
        ICommandHandler<TakeTimeSlotTimeOffCommand> takeTimeSlotTimeOffCommandHandler)
    {
        this.addTimeSlotAvailabilityCommandHandler = addTimeSlotAvailabilityCommandHandler;
        this.takeTimeSlotTimeOffCommandHandler = takeTimeSlotTimeOffCommandHandler;
    }

    [HttpPost]
    public async Task<ActionResult> AddAvailability(AddTimeSlotAvailabilityCommand command, CancellationToken cancellationToken)
        => await Handle(addTimeSlotAvailabilityCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> TakeTimeOff(TakeTimeSlotTimeOffCommand command, CancellationToken cancellationToken)
        => await Handle(takeTimeSlotTimeOffCommandHandler, command, cancellationToken);
}
