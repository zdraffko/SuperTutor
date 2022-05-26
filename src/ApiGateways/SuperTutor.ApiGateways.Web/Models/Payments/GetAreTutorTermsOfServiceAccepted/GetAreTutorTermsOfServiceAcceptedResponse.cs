using System.Text.Json.Serialization;

namespace SuperTutor.ApiGateways.Web.Models.Payments.GetAreTutorTermsOfServiceAccepted;

public class GetAreTutorTermsOfServiceAcceptedResponse
{
    [JsonPropertyName("areTutorTermsOfServiceAccepted")]
    public bool AreTutorTermsOfServiceAccepted { get; init; }
}
