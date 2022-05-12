using System.Text.Json.Serialization;

namespace SuperTutor.ApiGateways.Web.Models.Identity.Register;

public class RegisterResponse
{
    [JsonPropertyName("authToken")]
    public string? AuthToken { get; set; }
}
