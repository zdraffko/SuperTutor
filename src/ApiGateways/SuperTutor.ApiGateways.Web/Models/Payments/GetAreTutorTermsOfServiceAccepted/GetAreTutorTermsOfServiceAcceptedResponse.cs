using System.Text.Json.Serialization;

namespace SuperTutor.ApiGateways.Web.Models.Payments.GetAreTutorTermsOfServiceAccepted;

public class GetAreTutorTermsOfServiceAcceptedResponse
{
    [JsonPropertyName("areTermsOfServiceAccepted")]
    public bool AreTutorTermsOfServiceAccepted { get; init; }
}
