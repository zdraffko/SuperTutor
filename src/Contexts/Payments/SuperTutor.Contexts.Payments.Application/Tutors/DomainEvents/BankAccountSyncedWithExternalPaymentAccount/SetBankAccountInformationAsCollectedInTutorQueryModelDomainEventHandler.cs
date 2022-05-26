using SuperTutor.Contexts.Payments.Application.Tutors.Shared;
using SuperTutor.Contexts.Payments.Domain.Tutors.Events;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.DomainEvents;

namespace SuperTutor.Contexts.Payments.Application.Tutors.DomainEvents.BankAccountSyncedWithExternalPaymentAccount;

internal class SetBankAccountInformationAsCollectedInTutorQueryModelDomainEventHandler : IDomainEventHandler<TutorBankAccountSyncedWithExternalPaymentAccountDomainEvent>
{
    private readonly ITutorQueryModelRepository tutorQueryModelRepository;

    public SetBankAccountInformationAsCollectedInTutorQueryModelDomainEventHandler(ITutorQueryModelRepository tutorQueryModelRepository) => this.tutorQueryModelRepository = tutorQueryModelRepository;

    public async Task Handle(TutorBankAccountSyncedWithExternalPaymentAccountDomainEvent domainEvent, CancellationToken cancellationToken)
        => await tutorQueryModelRepository.SetBankAccountInformationAsCollected(domainEvent.TutorId, cancellationToken);
}
