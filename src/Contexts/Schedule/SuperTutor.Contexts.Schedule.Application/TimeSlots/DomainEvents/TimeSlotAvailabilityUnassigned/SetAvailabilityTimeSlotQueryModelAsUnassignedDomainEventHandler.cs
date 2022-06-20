using SuperTutor.Contexts.Schedule.Application.TimeSlots.Shared;
using SuperTutor.Contexts.Schedule.Domain.TimeSlots.Events;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.DomainEvents;

namespace SuperTutor.Contexts.Schedule.Application.TimeSlots.DomainEvents.TimeSlotAvailabilityUnassigned;

internal class SetAvailabilityTimeSlotQueryModelAsUnassignedDomainEventHandler : IDomainEventHandler<TimeSlotAvailabilityUnassignedDomainEvent>
{
    private readonly ITimeSlotQueryModelRepository timeSlotQueryModelRepository;

    public SetAvailabilityTimeSlotQueryModelAsUnassignedDomainEventHandler(ITimeSlotQueryModelRepository timeSlotQueryModelRepository) => this.timeSlotQueryModelRepository = timeSlotQueryModelRepository;

    public async Task Handle(TimeSlotAvailabilityUnassignedDomainEvent domainEvent, CancellationToken cancellationToken)
        => await timeSlotQueryModelRepository.SetAvailabilityAsUnassigned(domainEvent.TimeSlotId, cancellationToken);
}
