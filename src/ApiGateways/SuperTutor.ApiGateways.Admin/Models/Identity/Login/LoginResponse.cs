using System.Text.Json.Serialization;

namespace SuperTutor.ApiGateways.Web.Models.Identity.Login;

public class LoginResponse
{
    [JsonPropertyName("authToken")]
    public string? AuthToken { get; set; }
}
