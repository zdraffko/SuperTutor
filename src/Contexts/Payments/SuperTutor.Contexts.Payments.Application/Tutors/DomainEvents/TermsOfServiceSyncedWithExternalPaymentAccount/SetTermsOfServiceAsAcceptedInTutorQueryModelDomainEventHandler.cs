using SuperTutor.Contexts.Payments.Application.Tutors.Shared;
using SuperTutor.Contexts.Payments.Domain.Tutors.Events;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.DomainEvents;

namespace SuperTutor.Contexts.Payments.Application.Tutors.DomainEvents.TermsOfServiceSyncedWithExternalPaymentAccount;

internal class SetTermsOfServiceAsAcceptedInTutorQueryModelDomainEventHandler : IDomainEventHandler<TutorTermsOfServiceSyncedWithExternalPaymentAccountDomainEvent>
{
    private readonly ITutorQueryModelRepository tutorQueryModelRepository;

    public SetTermsOfServiceAsAcceptedInTutorQueryModelDomainEventHandler(ITutorQueryModelRepository tutorQueryModelRepository) => this.tutorQueryModelRepository = tutorQueryModelRepository;

    public async Task Handle(TutorTermsOfServiceSyncedWithExternalPaymentAccountDomainEvent domainEvent, CancellationToken cancellationToken)
        => await tutorQueryModelRepository.SetTermsOfServiceAsAccepted(domainEvent.TutorId, cancellationToken);
}
