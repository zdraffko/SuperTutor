using System.Text.Json.Serialization;

namespace SuperTutor.ApiGateways.Web.Models.Payments.GetIsTutorAddressInformationCollected;

public class GetIsTutorAddressInformationCollectedResponse
{
    [JsonPropertyName("isTutorAddressInformationCollected")]
    public bool IsTutorAddressInformationCollected { get; init; }
}
