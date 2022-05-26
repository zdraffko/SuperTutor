using SuperTutor.Contexts.Payments.Application.Shared;
using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.Contexts.Payments.Domain.Tutors.Events;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.DomainEvents;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

namespace SuperTutor.Contexts.Payments.Application.Tutors.DomainEvents.VerificationDocumentsUploaded;

internal class UpdateExternalPaymentAccountVerificationDocumentsDomainEventHandler : IDomainEventHandler<TutorVerificationDocumentsUploadedDomainEvent>
{
    private readonly ITutorExternalPaymentService tutorExternalPaymentService;
    private readonly IAggregateRootEventsRepository<Tutor, TutorId, Guid> tutorRepository;

    public UpdateExternalPaymentAccountVerificationDocumentsDomainEventHandler(ITutorExternalPaymentService tutorExternalPaymentService, IAggregateRootEventsRepository<Tutor, TutorId, Guid> tutorRepository)
    {
        this.tutorExternalPaymentService = tutorExternalPaymentService;
        this.tutorRepository = tutorRepository;
    }

    public async Task Handle(TutorVerificationDocumentsUploadedDomainEvent domainEvent, CancellationToken cancellationToken)
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

        if (tutor.IdentityVerificationDocumentFront is null || tutor.IdentityVerificationDocumentBack is null || tutor.AddressVerificationDocument is null)
        {
            return;
        }

        var verificationDocumentsUpdateResult = await tutorExternalPaymentService.UpdateVerificationDocuments(tutor.ExternalPaymentAccount.Id, tutor.ExternalPaymentAccount.PersonId, tutor.IdentityVerificationDocumentFront, tutor.IdentityVerificationDocumentBack, tutor.AddressVerificationDocument, cancellationToken);
        if (verificationDocumentsUpdateResult.IsFailed)
        {
            return;
        }

        tutor.MarkVerificationDocumentsAsSyncedWithExternalPaymentAccount();

        await tutorRepository.Update(tutor, cancellationToken);
    }
}
