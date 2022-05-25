using SuperTutor.Contexts.Payments.Application.Shared;
using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.Contexts.Payments.Domain.Tutors.Events;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.DomainEvents;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

namespace SuperTutor.Contexts.Payments.Application.Tutors.DomainEvents.PersonalInformationUpdated;

internal class UpdaeExternalPaymentAccountPersonalInformationDomainEventHandler : IDomainEventHandler<TutorPersonalInformationUpdatedDomainEvent>
{
    private readonly ITutorExternalPaymentService tutorExternalPaymentService;
    private readonly IAggregateRootEventsRepository<Tutor, TutorId, Guid> tutorRepository;

    public UpdaeExternalPaymentAccountPersonalInformationDomainEventHandler(ITutorExternalPaymentService tutorExternalPaymentService, IAggregateRootEventsRepository<Tutor, TutorId, Guid> tutorRepository)
    {
        this.tutorExternalPaymentService = tutorExternalPaymentService;
        this.tutorRepository = tutorRepository;
    }

    public async Task Handle(TutorPersonalInformationUpdatedDomainEvent domainEvent, CancellationToken cancellationToken)
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

        if (tutor.PersonalInformation is null)
        {
            return;
        }

        var personalInformationUpdateResult = await tutorExternalPaymentService.UpdatePersonalInformation(tutor.ExternalPaymentAccount.Id, tutor.ExternalPaymentAccount.PersonId, tutor.PersonalInformation, cancellationToken);
        if (personalInformationUpdateResult.IsFailed)
        {
            return;
        }

        tutor.MarkPersonalInformationAsSyncedWithExternalPaymentAccount();

        await tutorRepository.Update(tutor, cancellationToken);
    }
}
