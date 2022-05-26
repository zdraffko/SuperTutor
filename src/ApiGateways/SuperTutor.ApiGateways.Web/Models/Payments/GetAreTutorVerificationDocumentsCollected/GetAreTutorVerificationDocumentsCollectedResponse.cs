using System.Text.Json.Serialization;

namespace SuperTutor.ApiGateways.Web.Models.Payments.GetAreTutorVerificationDocumentsCollected;

public class GetAreTutorVerificationDocumentsCollectedResponse
{
    [JsonPropertyName("areVerificationDocumentsCollected")]
    public bool AreVerificationDocumentsCollected { get; init; }
}
