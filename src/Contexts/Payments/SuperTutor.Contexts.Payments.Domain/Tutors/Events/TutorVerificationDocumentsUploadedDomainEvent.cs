using SuperTutor.Contexts.Payments.Domain.Tutors.Models.ValueObjects;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Payments.Domain.Tutors.Events;

public class TutorVerificationDocumentsUploadedDomainEvent : DomainEvent
{
    public TutorVerificationDocumentsUploadedDomainEvent(TutorId tutorId, Document identityVerificationDocumentFront, Document identityVerificationDocumentBack, Document addressVerificationDocument, bool areVerificationDocumentsSyncedWithExternalPaymentAccount)
    {
        TutorId = tutorId;
        IdentityVerificationDocumentFront = identityVerificationDocumentFront;
        IdentityVerificationDocumentBack = identityVerificationDocumentBack;
        AddressVerificationDocument = addressVerificationDocument;
        AreVerificationDocumentsSyncedWithExternalPaymentAccount = areVerificationDocumentsSyncedWithExternalPaymentAccount;
    }

    public TutorId TutorId { get; }

    public Document IdentityVerificationDocumentFront { get; }

    public Document IdentityVerificationDocumentBack { get; }

    public Document AddressVerificationDocument { get; }

    public bool AreVerificationDocumentsSyncedWithExternalPaymentAccount { get; }
}
