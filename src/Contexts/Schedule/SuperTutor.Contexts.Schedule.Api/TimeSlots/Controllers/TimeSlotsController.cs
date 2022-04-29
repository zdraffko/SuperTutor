using Microsoft.AspNetCore.Mvc;
using SuperTutor.Contexts.Schedule.Application.TimeSlots.Commands.AddAvailability;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Schedule.Api.TimeSlots.Controllers;

public class TimeSlotsController : ApiController
{
    private readonly ICommandHandler<AddTimeSlotAvailabilityCommand> addTimeSlotAvailabilityCommandHandler;

    public TimeSlotsController(ICommandHandler<AddTimeSlotAvailabilityCommand> addTimeSlotAvailabilityCommandHandler) => this.addTimeSlotAvailabilityCommandHandler = addTimeSlotAvailabilityCommandHandler;

    [HttpPost]
    public async Task<ActionResult> AddAvailability(AddTimeSlotAvailabilityCommand command, CancellationToken cancellationToken)
        => await Handle(addTimeSlotAvailabilityCommandHandler, command, cancellationToken);
}
