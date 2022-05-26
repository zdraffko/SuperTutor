using SuperTutor.Contexts.Payments.Application.Tutors.Shared;
using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.Contexts.Payments.Domain.Tutors.Events;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.DomainEvents;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

namespace SuperTutor.Contexts.Payments.Application.Tutors.DomainEvents.TermsOfServiceAccepted;

internal class UpdateExternalPaymentAccountTermsOfServiceDomainEventHandler : IDomainEventHandler<TutorTermsOfServiceAcceptedDomainEvent>
{
    private readonly ITutorExternalPaymentService tutorExternalPaymentService;
    private readonly IAggregateRootEventsRepository<Tutor, TutorId, Guid> tutorRepository;

    public UpdateExternalPaymentAccountTermsOfServiceDomainEventHandler(ITutorExternalPaymentService tutorExternalPaymentService, IAggregateRootEventsRepository<Tutor, TutorId, Guid> tutorRepository)
    {
        this.tutorExternalPaymentService = tutorExternalPaymentService;
        this.tutorRepository = tutorRepository;
    }

    public async Task Handle(TutorTermsOfServiceAcceptedDomainEvent domainEvent, CancellationToken cancellationToken)
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

        if (tutor.TermsOfService is null)
        {
            return;
        }

        var termsOfServiceUpdateResult = await tutorExternalPaymentService.UpdateTermsOfService(tutor.ExternalPaymentAccount.Id, tutor.TermsOfService, cancellationToken);
        if (termsOfServiceUpdateResult.IsFailed)
        {
            return;
        }

        tutor.MarkTermsOfServiceAsSyncedWithExternalPaymentAccount();

        await tutorRepository.Update(tutor, cancellationToken);
    }
}
