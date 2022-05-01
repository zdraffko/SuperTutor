using FluentResults;
using SuperTutor.Contexts.Schedule.Domain.TimeSlots;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

namespace SuperTutor.Contexts.Schedule.Application.TimeSlots.Commands.RemoveAvailability;

internal class RemoveTimeSlotAvailabilityCommandHandler : ICommandHandler<RemoveTimeSlotAvailabilityCommand>
{
    private readonly IAggregateRootEventsRepository<TimeSlot, TimeSlotId, Guid> timeSlotRepository;

    public RemoveTimeSlotAvailabilityCommandHandler(IAggregateRootEventsRepository<TimeSlot, TimeSlotId, Guid> timeSlotRepository) => this.timeSlotRepository = timeSlotRepository;

    public async Task<Result> Handle(RemoveTimeSlotAvailabilityCommand command, CancellationToken cancellationToken)
    {
        var timeSlot = await timeSlotRepository.Load(command.TimeSlotId, cancellationToken);
        if (timeSlot is null)
        {
            return Result.Ok();
        }

        timeSlot.RemoveAvailability();

        await timeSlotRepository.Update(timeSlot, cancellationToken);

        return Result.Ok();
    }
}
