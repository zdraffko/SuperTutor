using SuperTutor.Contexts.Payments.Application.Shared;
using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.Contexts.Payments.Domain.Tutors.Events;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.DomainEvents;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

namespace SuperTutor.Contexts.Payments.Application.Tutors.DomainEvents.BankAccountUpdated;

internal class UpdateExternalPaymentAccountBankAccountDomainEventHandler : IDomainEventHandler<TutorBankAccountUpdatedDomainEvent>
{
    private readonly ITutorExternalPaymentService tutorExternalPaymentService;
    private readonly IAggregateRootEventsRepository<Tutor, TutorId, Guid> tutorRepository;

    public UpdateExternalPaymentAccountBankAccountDomainEventHandler(ITutorExternalPaymentService tutorExternalPaymentService, IAggregateRootEventsRepository<Tutor, TutorId, Guid> tutorRepository)
    {
        this.tutorExternalPaymentService = tutorExternalPaymentService;
        this.tutorRepository = tutorRepository;
    }

    public async Task Handle(TutorBankAccountUpdatedDomainEvent domainEvent, CancellationToken cancellationToken)
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

        if (tutor.BankAccount is null)
        {
            return;
        }

        var bankAccountUpdateResult = await tutorExternalPaymentService.UpdateBankAccount(tutor.ExternalPaymentAccount.Id, tutor.BankAccount, cancellationToken);
        if (bankAccountUpdateResult.IsFailed)
        {
            return;
        }

        tutor.MarkBankAccountAsSyncedWithExternalPaymentAccount();

        await tutorRepository.Update(tutor, cancellationToken);
    }
}
