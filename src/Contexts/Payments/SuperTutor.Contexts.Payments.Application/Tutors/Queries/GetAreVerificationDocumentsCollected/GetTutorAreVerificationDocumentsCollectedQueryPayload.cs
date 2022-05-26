namespace SuperTutor.Contexts.Payments.Application.Tutors.Queries.GetAreVerificationDocumentsCollected;

public class GetTutorAreVerificationDocumentsCollectedQueryPayload
{
    public GetTutorAreVerificationDocumentsCollectedQueryPayload(bool areVerificationDocumentsCollected) => AreVerificationDocumentsCollected = areVerificationDocumentsCollected;

    public bool AreVerificationDocumentsCollected { get; }
}
