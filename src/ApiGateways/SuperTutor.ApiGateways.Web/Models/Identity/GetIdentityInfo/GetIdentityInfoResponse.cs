using System.Text.Json.Serialization;

namespace SuperTutor.ApiGateways.Web.Models.Identity.GetIdentityInfo;

public class GetIdentityInfoResponse
{
    [JsonPropertyName("userEmail")]
    public string? UserEmail { get; set; }

    [JsonPropertyName("userType")]
    public int UserType { get; set; }
}
