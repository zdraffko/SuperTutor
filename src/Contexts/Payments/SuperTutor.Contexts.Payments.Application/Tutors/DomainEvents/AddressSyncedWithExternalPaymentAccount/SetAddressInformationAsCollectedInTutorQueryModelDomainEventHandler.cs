using SuperTutor.Contexts.Payments.Application.Tutors.Shared;
using SuperTutor.Contexts.Payments.Domain.Tutors.Events;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.DomainEvents;

namespace SuperTutor.Contexts.Payments.Application.Tutors.DomainEvents.AddressSyncedWithExternalPaymentAccount;

internal class SetAddressInformationAsCollectedInTutorQueryModelDomainEventHandler : IDomainEventHandler<TutorAddressSyncedWithExternalPaymentAccountDomainEvent>
{
    private readonly ITutorQueryModelRepository tutorQueryModelRepository;

    public SetAddressInformationAsCollectedInTutorQueryModelDomainEventHandler(ITutorQueryModelRepository tutorQueryModelRepository) => this.tutorQueryModelRepository = tutorQueryModelRepository;

    public async Task Handle(TutorAddressSyncedWithExternalPaymentAccountDomainEvent domainEvent, CancellationToken cancellationToken)
        => await tutorQueryModelRepository.SetAddressInformationAsCollected(domainEvent.TutorId, cancellationToken);
}
