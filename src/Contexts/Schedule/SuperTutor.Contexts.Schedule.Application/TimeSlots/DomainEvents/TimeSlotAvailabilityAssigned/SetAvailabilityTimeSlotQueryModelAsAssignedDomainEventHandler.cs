using SuperTutor.Contexts.Schedule.Application.TimeSlots.Shared;
using SuperTutor.Contexts.Schedule.Domain.TimeSlots.Events;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.DomainEvents;

namespace SuperTutor.Contexts.Schedule.Application.TimeSlots.DomainEvents.TimeSlotAvailabilityAssigned;

internal class SetAvailabilityTimeSlotQueryModelAsAssignedDomainEventHandler : IDomainEventHandler<TimeSlotAvailabilityAssignedDomainEvent>
{
    private readonly ITimeSlotQueryModelRepository timeSlotQueryModelRepository;

    public SetAvailabilityTimeSlotQueryModelAsAssignedDomainEventHandler(ITimeSlotQueryModelRepository timeSlotQueryModelRepository) => this.timeSlotQueryModelRepository = timeSlotQueryModelRepository;

    public async Task Handle(TimeSlotAvailabilityAssignedDomainEvent domainEvent, CancellationToken cancellationToken)
        => await timeSlotQueryModelRepository.SetAvailabilityAsAssigned(domainEvent.TimeSlotId, cancellationToken);
}
