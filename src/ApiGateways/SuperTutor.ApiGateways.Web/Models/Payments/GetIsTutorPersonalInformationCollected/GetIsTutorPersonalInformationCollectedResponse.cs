using System.Text.Json.Serialization;

namespace SuperTutor.ApiGateways.Web.Models.Payments.GetIsTutorPersonalInformationCollected;

public class GetIsTutorPersonalInformationCollectedResponse
{
    [JsonPropertyName("isTutorPersonalInformationCollected")]
    public bool IsTutorPersonalInformationCollected { get; init; }
}
