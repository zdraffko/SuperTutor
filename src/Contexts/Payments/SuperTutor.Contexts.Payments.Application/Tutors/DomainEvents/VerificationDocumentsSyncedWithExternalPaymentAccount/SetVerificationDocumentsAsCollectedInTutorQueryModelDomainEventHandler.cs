using SuperTutor.Contexts.Payments.Application.Tutors.Shared;
using SuperTutor.Contexts.Payments.Domain.Tutors.Events;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.DomainEvents;

namespace SuperTutor.Contexts.Payments.Application.Tutors.DomainEvents.AddressSyncedWithExternalPaymentAccount;

internal class SetVerificationDocumentsAsCollectedInTutorQueryModelDomainEventHandler : IDomainEventHandler<TutorVerificationDocumentsSyncedWithExternalPaymentAccountDomainEvent>
{
    private readonly ITutorQueryModelRepository tutorQueryModelRepository;

    public SetVerificationDocumentsAsCollectedInTutorQueryModelDomainEventHandler(ITutorQueryModelRepository tutorQueryModelRepository) => this.tutorQueryModelRepository = tutorQueryModelRepository;

    public async Task Handle(TutorVerificationDocumentsSyncedWithExternalPaymentAccountDomainEvent domainEvent, CancellationToken cancellationToken)
        => await tutorQueryModelRepository.SetVerificationDocumentsAsCollected(domainEvent.TutorId, cancellationToken);
}
