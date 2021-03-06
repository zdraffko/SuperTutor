using System.Text.Json.Serialization;

namespace SuperTutor.ApiGateways.Web.Models.Identity.GetIdentityInfo;

public class GetIdentityInfoResponse
{
    [JsonPropertyName("userId")]
    public string? UserId { get; set; }

    [JsonPropertyName("userEmail")]
    public string? UserEmail { get; set; }

    [JsonPropertyName("firstName")]
    public string? FirstName { get; set; }

    [JsonPropertyName("lastName")]
    public string? LastName { get; set; }
}
