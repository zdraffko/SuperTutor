using System.Text.Json.Serialization;

namespace SuperTutor.ApiGateways.Web.Models.Payments.GetIsTutorBankAccountInformationCollected;

public class GetIsTutorBankAccountInformationCollectedResponse
{
    [JsonPropertyName("isTutorBankAccountInformationCollected")]
    public bool IsTutorBankAccountInformationCollected { get; init; }
}
