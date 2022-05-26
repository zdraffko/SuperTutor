namespace SuperTutor.Contexts.Payments.Application.Tutors.Queries.GetIsBankAccountInformationCollected;

public class GetTutorIsBankAccountInformationCollectedQueryPayload
{
    public GetTutorIsBankAccountInformationCollectedQueryPayload(bool isBankAccountInformationCollected) => IsBankAccountInformationCollected = isBankAccountInformationCollected;

    public bool IsBankAccountInformationCollected { get; }
}
