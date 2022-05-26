namespace SuperTutor.Contexts.Payments.Application.Tutors.Queries.GetIsAddressInformationCollected;

public class GetTutorIsAddressInformationCollectedQueryPayload
{
    public GetTutorIsAddressInformationCollectedQueryPayload(bool isAddressInformationCollected) => IsAddressInformationCollected = isAddressInformationCollected;

    public bool IsAddressInformationCollected { get; }
}
