namespace SuperTutor.Contexts.Payments.Application.Tutors.Queries.GetIsPersonalInformationCollected;

public class GetTutorIsPersonalInformationCollectedQueryPayload
{
    public GetTutorIsPersonalInformationCollectedQueryPayload(bool isPersonalInformationCollected) => IsPersonalInformationCollected = isPersonalInformationCollected;

    public bool IsPersonalInformationCollected { get; }
}
