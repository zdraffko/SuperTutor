using FluentResults;
using SuperTutor.Contexts.Schedule.Domain.TimeSlots;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

namespace SuperTutor.Contexts.Schedule.Application.TimeSlots.Commands.AddAvailability;

internal class AddTimeSlotAvailabilityCommandHandler : ICommandHandler<AddTimeSlotAvailabilityCommand>
{
    private readonly IAggregateRootEventsRepository<TimeSlot, TimeSlotId, Guid> timeSlotRepository;

    public AddTimeSlotAvailabilityCommandHandler(IAggregateRootEventsRepository<TimeSlot, TimeSlotId, Guid> timeSlotRepository) => this.timeSlotRepository = timeSlotRepository;

    public async Task<Result> Handle(AddTimeSlotAvailabilityCommand command, CancellationToken cancellationToken)
    {
        var timeSlot = TimeSlot.AddAvailability(command.TutorId, command.Date, command.StartTime);

        await timeSlotRepository.Add(timeSlot, cancellationToken);

        return Result.Ok();
    }
}
