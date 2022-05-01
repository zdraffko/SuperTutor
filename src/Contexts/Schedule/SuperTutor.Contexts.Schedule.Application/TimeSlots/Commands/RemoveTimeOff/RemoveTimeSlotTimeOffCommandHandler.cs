using FluentResults;
using SuperTutor.Contexts.Schedule.Domain.TimeSlots;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

namespace SuperTutor.Contexts.Schedule.Application.TimeSlots.Commands.RemoveTimeOff;

internal class RemoveTimeSlotTimeOffCommandHandler : ICommandHandler<RemoveTimeSlotTimeOffCommand>
{
    private readonly IAggregateRootEventsRepository<TimeSlot, TimeSlotId, Guid> timeSlotRepository;

    public RemoveTimeSlotTimeOffCommandHandler(IAggregateRootEventsRepository<TimeSlot, TimeSlotId, Guid> timeSlotRepository) => this.timeSlotRepository = timeSlotRepository;

    public async Task<Result> Handle(RemoveTimeSlotTimeOffCommand command, CancellationToken cancellationToken)
    {
        var timeSlot = await timeSlotRepository.Load(command.TimeSlotId, cancellationToken);
        if (timeSlot is null)
        {
            return Result.Ok();
        }

        timeSlot.RemoveTimeOff();

        await timeSlotRepository.Update(timeSlot, cancellationToken);

        return Result.Ok();
    }
}
