using Microsoft.AspNetCore.Mvc;
using SuperTutor.Contexts.Schedule.Application.TimeSlots.Commands.AddAvailability;
using SuperTutor.Contexts.Schedule.Application.TimeSlots.Commands.RemoveAvailability;
using SuperTutor.Contexts.Schedule.Application.TimeSlots.Commands.RemoveTimeOff;
using SuperTutor.Contexts.Schedule.Application.TimeSlots.Commands.TakeTimeOff;
using SuperTutor.Contexts.Schedule.Application.TimeSlots.Queries.GetAvailability;
using SuperTutor.Contexts.Schedule.Application.TimeSlots.Queries.GetForWeek;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Attributes;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Schedule.Api.TimeSlots.Controllers;

public class TimeSlotsController : ApiController
{
    private readonly ICommandHandler<AddTimeSlotAvailabilityCommand> addTimeSlotAvailabilityCommandHandler;
    private readonly ICommandHandler<RemoveTimeSlotAvailabilityCommand> removeTimeSlotAvailabilityCommandHandler;
    private readonly ICommandHandler<TakeTimeSlotTimeOffCommand> takeTimeSlotTimeOffCommandHandler;
    private readonly ICommandHandler<RemoveTimeSlotTimeOffCommand> removeTimeSlotTimeOffCommandHandler;
    private readonly IQueryHandler<GetTimeSlotsForWeekQuery, GetTimeSlotsForWeekQueryPayload> getTimeSlotsForWeekQueryHandler;
    private readonly IQueryHandler<GetTutorAvailabilityQuery, GetTutorAvailabilityQueryPayload> getTutorAvailabilityQueryHandler;

    public TimeSlotsController(
        ICommandHandler<AddTimeSlotAvailabilityCommand> addTimeSlotAvailabilityCommandHandler,
        ICommandHandler<RemoveTimeSlotAvailabilityCommand> removeTimeSlotAvailabilityCommandHandler,
        ICommandHandler<TakeTimeSlotTimeOffCommand> takeTimeSlotTimeOffCommandHandler,
        ICommandHandler<RemoveTimeSlotTimeOffCommand> removeTimeSlotTimeOffCommandHandler,
        IQueryHandler<GetTimeSlotsForWeekQuery, GetTimeSlotsForWeekQueryPayload> getTimeSlotsForWeekQueryHandler,
        IQueryHandler<GetTutorAvailabilityQuery, GetTutorAvailabilityQueryPayload> getTutorAvailabilityQueryHandler)
    {
        this.addTimeSlotAvailabilityCommandHandler = addTimeSlotAvailabilityCommandHandler;
        this.removeTimeSlotAvailabilityCommandHandler = removeTimeSlotAvailabilityCommandHandler;
        this.takeTimeSlotTimeOffCommandHandler = takeTimeSlotTimeOffCommandHandler;
        this.removeTimeSlotTimeOffCommandHandler = removeTimeSlotTimeOffCommandHandler;
        this.getTimeSlotsForWeekQueryHandler = getTimeSlotsForWeekQueryHandler;
        this.getTutorAvailabilityQueryHandler = getTutorAvailabilityQueryHandler;
    }

    [HttpPost]
    public async Task<ActionResult> AddAvailability(AddTimeSlotAvailabilityCommand command, CancellationToken cancellationToken)
        => await Handle(addTimeSlotAvailabilityCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> RemoveAvailability(RemoveTimeSlotAvailabilityCommand command, CancellationToken cancellationToken)
        => await Handle(removeTimeSlotAvailabilityCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> TakeTimeOff(TakeTimeSlotTimeOffCommand command, CancellationToken cancellationToken)
        => await Handle(takeTimeSlotTimeOffCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> RemoveTimeOff(RemoveTimeSlotTimeOffCommand command, CancellationToken cancellationToken)
        => await Handle(removeTimeSlotTimeOffCommandHandler, command, cancellationToken);

    [HttpGet]
    public async Task<ActionResult<GetTimeSlotsForWeekQueryPayload>> GetForWeek([FromJsonQuery] GetTimeSlotsForWeekQuery query, CancellationToken cancellationToken)
        => await Handle(getTimeSlotsForWeekQueryHandler, query, cancellationToken);

    [HttpGet]
    public async Task<ActionResult<GetTutorAvailabilityQueryPayload>> GetAvailability([FromJsonQuery] GetTutorAvailabilityQuery query, CancellationToken cancellationToken)
        => await Handle(getTutorAvailabilityQueryHandler, query, cancellationToken);
}
