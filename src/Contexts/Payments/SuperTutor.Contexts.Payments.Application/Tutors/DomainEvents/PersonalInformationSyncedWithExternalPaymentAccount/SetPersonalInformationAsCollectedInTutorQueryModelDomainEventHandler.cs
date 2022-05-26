using SuperTutor.Contexts.Payments.Application.Tutors.Shared;
using SuperTutor.Contexts.Payments.Domain.Tutors.Events;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.DomainEvents;

namespace SuperTutor.Contexts.Payments.Application.Tutors.DomainEvents.AddressSyncedWithExternalPaymentAccount;

internal class SetPersonalInformationAsCollectedInTutorQueryModelDomainEventHandler : IDomainEventHandler<TutorPersonalInformationSyncedWithExternalPaymentAccountDomainEvent>
{
    private readonly ITutorQueryModelRepository tutorQueryModelRepository;

    public SetPersonalInformationAsCollectedInTutorQueryModelDomainEventHandler(ITutorQueryModelRepository tutorQueryModelRepository) => this.tutorQueryModelRepository = tutorQueryModelRepository;

    public async Task Handle(TutorPersonalInformationSyncedWithExternalPaymentAccountDomainEvent domainEvent, CancellationToken cancellationToken)
        => await tutorQueryModelRepository.SetPersonalInformationAsCollected(domainEvent.TutorId, cancellationToken);
}
