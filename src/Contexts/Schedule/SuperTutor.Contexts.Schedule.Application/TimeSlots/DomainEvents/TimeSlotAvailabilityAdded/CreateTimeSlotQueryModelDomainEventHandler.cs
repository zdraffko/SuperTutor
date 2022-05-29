using SuperTutor.Contexts.Schedule.Application.TimeSlots.Shared;
using SuperTutor.Contexts.Schedule.Domain.TimeSlots.Events;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.DomainEvents;

namespace SuperTutor.Contexts.Schedule.Application.TimeSlots.DomainEvents.TimeSlotAvailabilityAdded;

internal class CreateTimeSlotQueryModelDomainEventHandler : IDomainEventHandler<TimeSlotAvailabilityAddedDomainEvent>
{
    private readonly ITimeSlotQueryModelRepository timeSlotQueryModelRepository;

    public CreateTimeSlotQueryModelDomainEventHandler(ITimeSlotQueryModelRepository timeSlotQueryModelRepository) => this.timeSlotQueryModelRepository = timeSlotQueryModelRepository;

    public async Task Handle(TimeSlotAvailabilityAddedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var timeSlotQueryModel = new TimeSlotQueryModel
        {
            Id = domainEvent.TimeSlotId,
            TutorId = domainEvent.TutorId,
            Date = domainEvent.Date.ToDateTime(domainEvent.StartTime),
            StartTime = domainEvent.StartTime.ToTimeSpan(),
            Type = domainEvent.Type.Name,
            Status = domainEvent.Status.Name
        };

        await timeSlotQueryModelRepository.Create(timeSlotQueryModel, cancellationToken);
    }
}
