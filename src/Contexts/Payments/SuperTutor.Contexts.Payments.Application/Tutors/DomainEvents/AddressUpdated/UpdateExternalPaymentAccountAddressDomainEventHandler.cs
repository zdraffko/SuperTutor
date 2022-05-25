using SuperTutor.Contexts.Payments.Application.Shared;
using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.Contexts.Payments.Domain.Tutors.Events;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.DomainEvents;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

namespace SuperTutor.Contexts.Payments.Application.Tutors.DomainEvents.AddressUpdated;

internal class UpdateExternalPaymentAccountAddressDomainEventHandler : IDomainEventHandler<TutorAddressUpdatedDomainEvent>
{
    private readonly ITutorExternalPaymentService tutorExternalPaymentService;
    private readonly IAggregateRootEventsRepository<Tutor, TutorId, Guid> tutorRepository;

    public UpdateExternalPaymentAccountAddressDomainEventHandler(ITutorExternalPaymentService tutorExternalPaymentService, IAggregateRootEventsRepository<Tutor, TutorId, Guid> tutorRepository)
    {
        this.tutorExternalPaymentService = tutorExternalPaymentService;
        this.tutorRepository = tutorRepository;
    }

    public async Task Handle(TutorAddressUpdatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var tutor = await tutorRepository.Load(domainEvent.TutorId, cancellationToken);
        if (tutor is null)
        {
            return;
        }

        if (tutor.ExternalPaymentAccount is null)
        {
            return;
        }

        if (tutor.Address is null)
        {
            return;
        }

        var addressUpdateResult = await tutorExternalPaymentService.UpdateAddress(tutor.ExternalPaymentAccount.Id, tutor.ExternalPaymentAccount.PersonId, tutor.Address, cancellationToken);
        if (addressUpdateResult.IsFailed)
        {
            return;
        }

        tutor.MarkAddressAsSyncedWithExternalPaymentAccount();

        await tutorRepository.Update(tutor, cancellationToken);
    }
}
