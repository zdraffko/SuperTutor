using FluentResults;
using SuperTutor.Contexts.Payments.Application.Tutors.Shared;
using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.Contexts.Payments.Domain.Tutors.Models.ValueObjects;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

namespace SuperTutor.Contexts.Payments.Application.Tutors.Commands.UploadVerificationDocuments;

internal class UploadTutorVerificationDocumentsCommandHandler : ICommandHandler<UploadTutorVerificationDocumentsCommand>
{
    private readonly IAggregateRootEventsRepository<Tutor, TutorId, Guid> tutorRepository;
    private readonly ITutorExternalPaymentService tutorExternalPaymentService;

    public UploadTutorVerificationDocumentsCommandHandler(IAggregateRootEventsRepository<Tutor, TutorId, Guid> tutorRepository, ITutorExternalPaymentService tutorExternalPaymentService)
    {
        this.tutorRepository = tutorRepository;
        this.tutorExternalPaymentService = tutorExternalPaymentService;
    }

    public async Task<Result> Handle(UploadTutorVerificationDocumentsCommand command, CancellationToken cancellationToken)
    {
        // TODO - Refactor the file upload
        var tutor = await tutorRepository.Load(command.TutorId, cancellationToken);
        if (tutor is null)
        {
            return Result.Fail($"Tutor with Id {command.TutorId} was not found");
        }

        var uploadIdentityDocumentFrontResult = await tutorExternalPaymentService.UploadIdentityDocument(command.IdentityDocumentFront, cancellationToken);
        var uploadIdentityDocumentBackResult = await tutorExternalPaymentService.UploadIdentityDocument(command.IdentityDocumentBack, cancellationToken);
        var uploadAddressDocumentResult = await tutorExternalPaymentService.UploadIdentityDocument(command.AddressDocument, cancellationToken);

        var identityVerificationDocumentFront = new Document(uploadIdentityDocumentFrontResult.Value.fileId, uploadIdentityDocumentFrontResult.Value.fileName, uploadIdentityDocumentFrontResult.Value.fileUrl);
        var identityVerificationDocumentBack = new Document(uploadIdentityDocumentBackResult.Value.fileId, uploadIdentityDocumentBackResult.Value.fileName, uploadIdentityDocumentBackResult.Value.fileUrl);
        var addressVerificationDocument = new Document(uploadAddressDocumentResult.Value.fileId, uploadAddressDocumentResult.Value.fileName, uploadAddressDocumentResult.Value.fileUrl);

        tutor.UploadVerificationDocuments(identityVerificationDocumentFront, identityVerificationDocumentBack, addressVerificationDocument);

        await tutorRepository.Update(tutor, cancellationToken);

        return Result.Ok();
    }
}
