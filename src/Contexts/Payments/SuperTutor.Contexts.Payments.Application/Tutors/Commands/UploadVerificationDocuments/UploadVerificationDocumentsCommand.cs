using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Payments.Application.Tutors.Commands.UploadVerificationDocuments;

public class UploadVerificationDocumentsCommand : Command
{
    public UploadVerificationDocumentsCommand(TutorId tutorId, Stream identityDocumentFront, Stream identityDocumentBack, Stream addressDocument)
    {
        TutorId = tutorId;
        IdentityDocumentFront = identityDocumentFront;
        IdentityDocumentBack = identityDocumentBack;
        AddressDocument = addressDocument;
    }

    public TutorId TutorId { get; }

    public Stream IdentityDocumentFront { get; }

    public Stream IdentityDocumentBack { get; }

    public Stream AddressDocument { get; }
}
