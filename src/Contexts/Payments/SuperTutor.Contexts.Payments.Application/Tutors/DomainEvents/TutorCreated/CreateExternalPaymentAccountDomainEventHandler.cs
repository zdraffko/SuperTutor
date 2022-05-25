using SuperTutor.Contexts.Payments.Application.Shared;
using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.Contexts.Payments.Domain.Tutors.Events;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.DomainEvents;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

namespace SuperTutor.Contexts.Payments.Application.Tutors.DomainEvents.TutorCreated;

internal class CreateExternalPaymentAccountDomainEventHandler : IDomainEventHandler<TutorCreatedDomainEvent>
{
    private readonly ITutorExternalPaymentService tutorExternalPaymentService;
    private readonly IAggregateRootEventsRepository<Tutor, TutorId, Guid> tutorRepository;

    public CreateExternalPaymentAccountDomainEventHandler(ITutorExternalPaymentService tutorExternalPaymentService, IAggregateRootEventsRepository<Tutor, TutorId, Guid> tutorRepository)
    {
        this.tutorExternalPaymentService = tutorExternalPaymentService;
        this.tutorRepository = tutorRepository;
    }

    public async Task Handle(TutorCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var createExternalPaymentAccountResult = await tutorExternalPaymentService.CreateAccount(domainEvent.TutorId, domainEvent.Email, cancellationToken);
        if (createExternalPaymentAccountResult.IsFailed)
        {
            // TODO - Trigger some process to handle this case
            return;
        }

        var tutor = await tutorRepository.Load(domainEvent.TutorId, cancellationToken);
        if (tutor is null)
        {
            return;
        }

        var (accountId, personId) = createExternalPaymentAccountResult.Value;
        tutor.CreateExternalPaymentAccount(accountId, personId);

        await tutorRepository.Update(tutor, cancellationToken);
    }
}
