using SuperTutor.Contexts.Payments.Application.Tutors.Shared;
using SuperTutor.Contexts.Payments.Domain.Tutors.Events;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.DomainEvents;

namespace SuperTutor.Contexts.Payments.Application.Tutors.DomainEvents.TutorCreated;

internal class CreateTutorQueryModelDomainEventHandler : IDomainEventHandler<TutorCreatedDomainEvent>
{
    private readonly ITutorQueryModelRepository tutorQueryModelRepository;

    public CreateTutorQueryModelDomainEventHandler(ITutorQueryModelRepository tutorQueryModelRepository) => this.tutorQueryModelRepository = tutorQueryModelRepository;

    public async Task Handle(TutorCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
        => await tutorQueryModelRepository.Create(domainEvent.TutorId, cancellationToken);
}
