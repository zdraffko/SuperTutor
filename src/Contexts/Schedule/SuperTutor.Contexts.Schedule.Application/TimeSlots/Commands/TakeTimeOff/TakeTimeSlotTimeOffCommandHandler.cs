using FluentResults;
using SuperTutor.Contexts.Schedule.Domain.TimeSlots;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

namespace SuperTutor.Contexts.Schedule.Application.TimeSlots.Commands.TakeTimeOff;

internal class TakeTimeSlotTimeOffCommandHandler : ICommandHandler<TakeTimeSlotTimeOffCommand>
{
    private readonly IAggregateRootEventsRepository<TimeSlot, TimeSlotId, Guid> timeSlotRepository;

    public TakeTimeSlotTimeOffCommandHandler(IAggregateRootEventsRepository<TimeSlot, TimeSlotId, Guid> timeSlotRepository) => this.timeSlotRepository = timeSlotRepository;

    public async Task<Result> Handle(TakeTimeSlotTimeOffCommand command, CancellationToken cancellationToken)
    {
        var timeSlot = TimeSlot.TakeTimeOff(command.TutorId, command.Date, command.StartTime);

        await timeSlotRepository.Add(timeSlot, cancellationToken);

        return Result.Ok();
    }
}
