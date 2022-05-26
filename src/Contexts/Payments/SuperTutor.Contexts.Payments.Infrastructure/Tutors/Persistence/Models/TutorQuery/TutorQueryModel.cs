using SuperTutor.Contexts.Payments.Domain.Tutors;

namespace SuperTutor.Contexts.Payments.Infrastructure.Tutors.Persistence.Models.TutorQuery;

public class TutorQueryModel
{
    public TutorId Id { get; init; }

    public bool IsPersonalInformationCollected { get; init; }

    public bool IsAddressInformationCollected { get; init; }

    public bool IsBankAccountInformationCollected { get; init; }

    public bool AreVerificationDocumentsCollected { get; init; }

    public bool AreTermsOfServiceAccepted { get; init; }
}
